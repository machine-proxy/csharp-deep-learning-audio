using System;
using csharp_deep_learning_audio;
using System.IO;
using System.Collections.Generic;

namespace csharp_deep_learning_audio_samples
{
    class SearchEngineDemo
    {
        public static void Demo()
        {
            
            AudioSearchEngine c = new AudioSearchEngine();
            string dataDirPath = Path.Combine(IOUtils.AssemblyDirectory, "..", "..", "..", "..", "gtzan", "genres");

            string[] subDirectories = Directory.GetDirectories(dataDirPath);
            foreach(string subDirectory in subDirectories)
            {
                string[] files = Directory.GetFiles(subDirectory, "*.au");
                foreach(string file in files)
                {
                    c.IndexAudio(file);
                }
            }

            int topK = 10;
            List<ComparableAudioEntry> result = c.Query(
                Path.Combine(IOUtils.AssemblyDirectory, "..", "..", "..", "..", "music-samples", "example.mp3"),
                topK);

            for(int i=0; i < result.Count; ++i)
            {
                Console.WriteLine("#{0} (Score: {1}): {2}", i + 1, result[i].Distance, result[i].AudioFile);
            }




        }
    }
}
