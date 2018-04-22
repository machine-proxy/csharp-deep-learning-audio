using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp_deep_learning_audio;
using System.IO;

namespace csharp_deep_learning_audio_samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Cifar10AudioClassifier c = new Cifar10AudioClassifier();
            string dataDirPath = @"C:\Users\chen0\git\csharp-deep-learning-audio\gtzan\genres";

            string[] subDirectories = Directory.GetDirectories(dataDirPath);
            foreach(string subDirectory in subDirectories)
            {
                string[] files = Directory.GetFiles(subDirectory, "*.au");
                foreach(string file in files)
                {
                    Console.WriteLine("classifing: {0}", file);
                    Console.WriteLine("predicted: {0}", c.PredictLabel(file));
                }
                
            }
        }
    }
}
