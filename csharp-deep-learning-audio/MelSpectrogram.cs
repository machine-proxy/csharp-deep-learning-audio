using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;
using Un4seen.Bass.Misc;

namespace csharp_deep_learning_audio
{
    public class MelSpectrogram
    {
        public int Width { get; set; } = 1366;
        public int Height { get; set; } = 96;

        private Bitmap DrawSpectrogram(string fileName, int width, int height, int stepsPerSecond)
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            int channel = Bass.BASS_StreamCreateFile(fileName, 0, 0, BASSFlag.BASS_DEFAULT);

            long len = Bass.BASS_ChannelGetLength(channel, BASSMode.BASS_POS_BYTES); // the length in bytes
            double time = Bass.BASS_ChannelBytes2Seconds(channel, len); // the length in seconds

            int total_steps = (int)Math.Floor(stepsPerSecond * time);

            

            int steps = Math.Min(width, total_steps);

            Console.WriteLine("number of seconds: {0} total steps: {1}, truncated steps: {2}", time, total_steps, steps);

            Visuals visuals = new Visuals();
            Bass.BASS_ChannelPlay(channel, false);
            Bitmap result = new Bitmap(steps, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                for (int i = 0; i < steps; i++)
                {
                    Bass.BASS_ChannelSetPosition(channel, 1.0 * i / stepsPerSecond);
                    visuals.CreateSpectrum3DVoicePrint(channel, g, new Rectangle(0, 0, result.Width, result.Height), Color.Black, Color.White, i, true, false);
                }
            }

            Bass.BASS_ChannelStop(channel);

            Bass.BASS_Stop();
            Bass.BASS_Free();

            return result;
        }

        private string Convert2Mp3(string file)
        {
            return file;
        }

        public static void InstallBass()
        {
            string dll_path = GetBassDllPath();
            if (!File.Exists(dll_path))
            {
                File.WriteAllBytes(dll_path, Properties.Resources.bass);
            }
        }

        public static void InstallFFMpeg()
        {
            InstallBinaryFile("avcodec-58.dll", Properties.Resources.avcodec_58);
            InstallBinaryFile("avdevice-58.dll", Properties.Resources.avdevice_58);
            InstallBinaryFile("avfilter-7.dll", Properties.Resources.avfilter_7);
            InstallBinaryFile("avformat-58.dll", Properties.Resources.avformat_58);
            InstallBinaryFile("avutil-56.dll", Properties.Resources.avutil_56);
            InstallBinaryFile("ffmpeg.exe", Properties.Resources.ffmpeg);
            InstallBinaryFile("ffplay.exe", Properties.Resources.ffplay);
            InstallBinaryFile("ffprobe.exe", Properties.Resources.ffprobe);
            InstallBinaryFile("postproc-55.dll", Properties.Resources.postproc_55);
            InstallBinaryFile("swresample-3.dll", Properties.Resources.swresample_3);
            InstallBinaryFile("swscale-5.dll", Properties.Resources.swscale_5);
        }

        private static void InstallBinaryFile(string filename, byte[] bytes)
        {
            string path = Path.Combine(AssemblyDirectory, filename);
            if (File.Exists(path)) return;
            File.WriteAllBytes(path, bytes);
        }

        public Bitmap Convert(string file)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine("{0} does not exists", file);
                return null;
            }

            bool converted = false;
            if (file.ToLower().EndsWith(".au"))
            {
                converted = true;
                file = Convert2Mp3(file);
            }

            InstallBass();
            InstallFFMpeg();
            
            Bitmap result = DrawSpectrogram(file, Width, Height, 8);

            if(converted)
            {
                File.Delete(file);
            }

            return result;
        }

        public static string GetBassDllPath()
        {
            return Path.Combine(AssemblyDirectory, "bass.dll");
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
