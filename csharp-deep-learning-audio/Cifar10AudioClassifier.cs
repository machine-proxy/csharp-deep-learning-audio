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
            runner.AddInput(graph["conv2d_1_input:0"][0], imageTensor);
            runner.Fetch(graph["output_node0:0"][0]);

            var output = runner.Run();

            // Fetch the results from output:
            TFTensor result = output[0];
            Console.WriteLine("shape: {0}", result.Shape);
            return 0;
        }
    }
}
