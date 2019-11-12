using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_sampler.Player
{
    public class SamplePage
    {
        public string Name { get; set; }
        public string Tone { get; set; }

        public Dictionary<int, SamplePool> Pools = new Dictionary<int, SamplePool>();

        public SamplePage(string root)
        {
            string[] directories = Directory.GetDirectories(root);

            Name = root.Substring(root.LastIndexOf('\\') + 1);

            foreach (string directory in directories)
            {
                String directoryName = directory.Substring(directory.LastIndexOf('\\') + 1);
                int index = -1;

                if (!Int32.TryParse(directoryName, out index))
                {
                    index = -1;
                }

                Pools.Add(index, new SamplePool(directory, index));
            }
        }

        private SamplePool GetSample(ButtonValue value)
        {
            var key = value.RawValue;

            return Pools.ContainsKey(key) ? Pools[key] : null;
        }

        public SamplePool GetSample(SampleIndex index)
        {
            return GetSample(index.ButtonValue);
        }

        public string GetSamplePath(ButtonValue value)
        {
            return GetSample(value)?.GetSamplePath() ?? "";
        }
    }
}
