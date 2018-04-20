using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp_deep_learning_audio;

namespace csharp_au_to_mp3_converter
{
    class Program
    {
        static void Main(string[] args)
        {
            MelSpectrogram gram = new MelSpectrogram();
            string dataDirPath = Path.Combine(IOUtils.AssemblyDirectory, "..", "..", "..", "gtzan", "genres");
            if (!Directory.Exists(dataDirPath))
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
                    string mp3file = "converted.mp3";
                    Console.WriteLine("Converting {0}", file);
                    FFMpeg.Convert2Mp3(file, mp3file);
                    if(!File.Exists(mp3file))
                    {
                        Console.WriteLine("Failed to convert to {0}", mp3file);
                    }
                    
                    break;
                }
                break;
            }
        }
    }
}
