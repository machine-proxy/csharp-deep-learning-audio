using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorFlow;

namespace csharp_deep_learning_audio
{
    public class AudioClassifier : IDisposable
    {
        private TFGraph graph = new TFGraph();
        private MelSpectrogram melgram = new MelSpectrogram();

        public AudioClassifier()
        {
            graph.Import(Properties.Resources.cifar10);
        }

        public void Dispose()
        {
            if (graph != null)
            {
                graph.Dispose();
                graph = null;
            }
        }

        public string PredictLabel(string audioFile)
        {
            return Genres.GetLabel(PredictClass(audioFile));
        }

        private static int ArgMax(float[] probabilities)
        {
            var bestIdx = -1;
            float best = float.MinValue;
            for (int i = 0; i < probabilities.Length; i++)
            {
                //Console.WriteLine("i: {0}, p: {1}", i, probabilities[i]);
                if (probabilities[i] > best)
                {
                    bestIdx = i;
                    best = probabilities[i];
                }
            }
            return bestIdx;
        }

        public float[] EncodeAudio(string audioFile)
        {
            Bitmap bitmap = melgram.Convert(audioFile, 48);

            TFTensor imageTensor = TensorUtils.GetImageTensor(bitmap, melgram.Width, melgram.Height);
            /*
            foreach(TFOperation op in graph.GetEnumerator())
            {
                if (op != null)
                {
                    Console.WriteLine(op.Name);
                }
            }*/
            var session = new TFSession(graph);
            var runner = session.GetRunner();
            runner.AddInput(graph["conv2d_1_input"][0], imageTensor);
            runner.Fetch(graph["output_node0"][0]);

            var output = runner.Run();

            // Fetch the results from output:
            TFTensor result = output[0];

            var rshape = result.Shape;
            if (result.NumDims != 2 || rshape[0] != 1)
            {
                var shape = "";
                foreach (var d in rshape)
                {
                    shape += $"{d} ";
                }
                shape = shape.Trim();
                Console.WriteLine($"Error: expected to produce a [1 N] shaped tensor where N is the number of labels, instead it produced one with shape [{shape}]");
                Environment.Exit(1);
            }


            bool jagged = true;
            
            if (jagged)
            {
                return ((float[][])result.GetValue(jagged: true))[0];
                
            }
            else
            {
                var val = (float[,])result.GetValue(jagged: false);
                float[] probabilities = new float[val.GetLength(1)];
                for (int i = 0; i < val.GetLength(1); i++)
                {
                    probabilities[i] = val[0, i];
                }
                return probabilities;
            }
        }

        public int PredictClass(string audioFile)
        {
            int bestIdx = ArgMax(EncodeAudio(audioFile));
            return bestIdx;
        }
    }
}
