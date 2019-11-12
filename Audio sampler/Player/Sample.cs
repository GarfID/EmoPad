namespace Audio_sampler.Player
{
    public class Sample
    {
        public readonly string Name;
        public readonly string Path;

        public Sample(string file)
        {
            Name = file.Substring(file.LastIndexOf('\\') + 1, file.LastIndexOf('.') - file.LastIndexOf('\\'));
            Path = file;
        }
    }
}