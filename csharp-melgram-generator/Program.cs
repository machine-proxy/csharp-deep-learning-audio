using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp_deep_learning_audio;

namespace csharp_melgram_generator
{
    class Program
    {
        static void Main(string[] args)
        {
            MelSpectrogram gram = new MelSpectrogram();
            string dataDirPath = Path.Combine(MelSpectrogram.AssemblyDirectory, "..", "..", "..", "gtzan", "genres");
            if(!Directory.Exists(dataDirPath))
            {
                Console.WriteLine("{0} does not exists", dataDirPath);
                return;
            }

            string[] subDirectories = Directory.GetDirectories(dataDirPath);
            foreach (string subDirectory in subDirectories)
            {
                string[] files = Directory.GetFiles(subDirectory, "*.au");
                foreach (string file in files)
                {
                    Console.WriteLine("Converting: {0}", file);
                    Bitmap img = gram.Convert(file);
                    img.Save(file + ".png");
                }

            }
        }
    }
}
