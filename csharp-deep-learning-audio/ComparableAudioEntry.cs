using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_deep_learning_audio
{
    public class ComparableAudioEntry : AudioEntry, IComparable<ComparableAudioEntry>
    {
        public ComparableAudioEntry(AudioEntry entry)
        {
            Vec = entry.Vec;
            AudioFile = entry.AudioFile;
        }
        public float Distance { get; set; }

        public int CompareTo(ComparableAudioEntry rhs)
        {
            return Distance.CompareTo(rhs.Distance);
        }
    }
}
