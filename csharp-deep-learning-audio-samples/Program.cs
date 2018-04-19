using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp_deep_learning_audio;

namespace csharp_deep_learning_audio_samples
{
    class Program
    {
        static void Main(string[] args)
        {
            MelSpectrogram gram = new MelSpectrogram();
            Bitmap img = gram.Convert(@"C:\Users\chen0\git\java-audio-encoding\mp3_samples\example.mp3");
            img.Save("output.png");
        }
    }
}
