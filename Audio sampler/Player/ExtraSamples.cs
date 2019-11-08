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
            if (sampleCursor + 8 < Samples.Count)
            {
                sampleCursor = sampleCursor + 9;
            }
            else
            {
                sampleCursor = 0;
            }
        }

        public void PrevBatch()
        {
            if (sampleCursor - 9 < 0)
            {
                sampleCursor = 0;
            }
            else
            {
                sampleCursor = sampleCursor - 9;
            }
        }

        internal string GetSamplePath(int index)
        {
            int pointer = sampleCursor + index - 1;
            if (Samples.ElementAtOrDefault(pointer) != null)
            {
                return Samples[pointer].Path;
            } else
            {
                return string.Empty;
            }
        }
    }
}
