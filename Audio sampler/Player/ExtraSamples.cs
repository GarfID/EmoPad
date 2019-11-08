using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_sampler.Player
{
    public class ExtraSamples
    {
        public int sampleCursor = 0;

        public List<Sample> Samples = new List<Sample>();

        public ExtraSamples(string root)
        {
            string[] files = Directory.GetFiles(root);

            foreach (string file in files)
            {
                Samples.Add(new Sample(file));
            }
        }

        public void NextBatch()
        {
            sampleCursor = sampleCursor + 9;
        }

        public void PrevBatch()
        {
            sampleCursor = sampleCursor - 9;
        }

        internal string GetSamplePath(int index)
        {
            int pointer = sampleCursor + index;
            if (Samples.ElementAtOrDefault(pointer - 1) != null)
            {
                return Samples[pointer - 1].Path;
            } else
            {
                return string.Empty;
            }
        }
    }
}
