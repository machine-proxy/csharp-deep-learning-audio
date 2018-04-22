using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_deep_learning_audio
{
    public class Genres
    {
        private static string[] labels = {"blues", "classical", "country", "disco", "hiphop", "jazz", "metal",
                "pop", "reggae", "rock"};

        public static string GetLabel(int classId)
        {
            if(classId >= 0 && classId < labels.Length)
            {
                return labels[classId];
            }
            return "unknown";
        }
    }
}
