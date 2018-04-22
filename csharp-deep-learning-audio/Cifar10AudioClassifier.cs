using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorFlow;

namespace csharp_deep_learning_audio
{
    public class Cifar10AudioClassifier : IDisposable
    {
        private TFGraph graph = new TFGraph();
        private MelSpectrogram melgram = new MelSpectrogram();

        public Cifar10AudioClassifier()
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

        public int PredictClass(string audioFile)
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
            var bestIdx = -1;
            float p = 0, best = float.MinValue;
            if (jagged)
            {
                var probabilities = ((float[][])result.GetValue(jagged: true))[0];
                for (int i = 0; i < probabilities.Length; i++)
                {
                    Console.WriteLine("i: {0}, p: {1}", i, probabilities[i]);
                    if (probabilities[i] > best)
                    {
                        bestIdx = i;
                        best = probabilities[i];
                    }
                }
            }
            else
            {
                var val = (float[,])result.GetValue(jagged: false);
                for (int i = 0; i < val.GetLength(1); i++)
                {
                    if (val[0, i] > best)
                    {
                        bestIdx = i;
                        best = val[0, i];
                    }
                }
            }


            return bestIdx;
        }
    }
}
