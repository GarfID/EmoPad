using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_sampler.Player
{
    public class Sample
    {
        public string Name;
        public string Path;

        public Sample()
        {
            this.Name = "test";
            this.Path = "E:\\test.mp3";
        }

        public Sample(string file)
        {
            this.Path = file;
        }

        public Sample(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }
    }
}
