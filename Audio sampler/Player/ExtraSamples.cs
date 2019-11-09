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
            if (sampleCursor + (SampleLibrary.PAGE_SIZE - 1) < Samples.Count)
            {
                sampleCursor += SampleLibrary.PAGE_SIZE;
            }
            else
            {
                sampleCursor = 0;
            }
        }

        public void PrevBatch()
        {
            if (sampleCursor - SampleLibrary.PAGE_SIZE < 0)
            {
                sampleCursor = 0;
            }
            else
            {
                sampleCursor -= SampleLibrary.PAGE_SIZE;
            }
        }

        internal Sample GetSample(int buttonValue)
        {
            var index = sampleCursor + buttonValue - 1;

            return index < Samples.Count ? Samples[index] : null;
        }

        internal string GetSamplePath(int buttonValue)
        {
            return GetSample(buttonValue)?.Path ?? string.Empty;
        }
    }
}
