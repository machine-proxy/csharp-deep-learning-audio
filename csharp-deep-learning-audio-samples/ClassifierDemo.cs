using System;
using csharp_deep_learning_audio;
using System.IO;

namespace csharp_deep_learning_audio_samples
{
    public class ClassifierDemo
    {
        public static void Demo()
        {
            AudioClassifier c = new AudioClassifier();
            string dataDirPath = Path.Combine(IOUtils.AssemblyDirectory, "..", "..", "..", "..", "gtzan", "genres");

            string[] subDirectories = Directory.GetDirectories(dataDirPath);
            foreach (string subDirectory in subDirectories)
            {
                string[] files = Directory.GetFiles(subDirectory, "*.au");
                foreach (string file in files)
                {
                    Console.WriteLine("Processing: {0}", file);
                    Console.WriteLine("Encoded Audio: {0}", c.EncodeAudio(file));
                    Console.WriteLine("predicted: {0}", c.PredictLabel(file));
                    break;
                }
            }
        }
    }
}
