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

        public Sample(string file)
        {
            this.Name = file.Substring(file.LastIndexOf('\\') + 1, (file.LastIndexOf('.') - file.LastIndexOf('\\')));
            this.Path = file;
        }

        public Sample(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }
    }
}
