using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_deep_learning_audio
{
    public class AudioSearchEngine : AudioClassifier
    {
        

        private List<AudioEntry> indexDb = new List<AudioEntry>();
        
        public void ClearAllIndices()
        {
            indexDb.Clear();
        }

        public float[] IndexAudio(string audioFile)
        {
            float[] vec = EncodeAudio(audioFile);
            indexDb.Add(new AudioEntry()
            {
                Vec = vec,
                AudioFile = audioFile
            });

            return vec;
        }

        public List<ComparableAudioEntry> Query(string audioFile, int topK)
        {
            float[] vec = EncodeAudio(audioFile);
            MaxPQ<ComparableAudioEntry> queue = new MaxPQ<ComparableAudioEntry>();
            foreach(AudioEntry entry in indexDb)
            {
                queue.Add(new ComparableAudioEntry(entry)
                {
                    Distance = ComputeDistance(entry.Vec, vec)
                });
                if(queue.Count > topK)
                {
                    queue.DelMax();
                }
            }
            List<ComparableAudioEntry> result = queue.ToList();
            result.Sort((a, b) => a.Distance.CompareTo(b.Distance));
            return result;
        }

        private static float ComputeDistance(float[] a1, float[] a2)
        {
            int len = a1.Length;
            float result = 0;
            for(int i=0; i < len; ++i)
            {
                result += (a1[i] - a2[i]) * (a1[i] - a2[i]);
            }
            return (float)Math.Sqrt(result);
        }
    }
}
