using System.Collections.Generic;
using System.IO;

namespace Audio_sampler.Player
{
    public class SamplePage
    {
        private readonly Dictionary<int, SamplePool> _pools = new Dictionary<int, SamplePool>();

        public SamplePage(string root)
        {
            var directories = Directory.GetDirectories(root);

            Name = root.Substring(root.LastIndexOf('\\') + 1);

            foreach (var directory in directories)
            {
                var directoryName = directory.Substring(directory.LastIndexOf('\\') + 1);
                var index = -1;

                if (!int.TryParse(directoryName, out index)) index = -1;

                _pools.Add(index, new SamplePool(directory, index));
            }
        }

        public string Name { get; }

        private SamplePool GetSample(ButtonValue value)
        {
            var key = value.RawValue;

            return _pools.ContainsKey(key) ? _pools[key] : null;
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