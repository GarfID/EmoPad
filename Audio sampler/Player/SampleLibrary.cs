using System.Collections.Generic;
using System.IO;

namespace Audio_sampler.Player
{
    public class SampleLibrary
    {
        private static SampleLibrary _instance;

        private int _selectedPage;
        public bool UseExtra;
        
        private List<SamplePage> _samplePages = new List<SamplePage>();



        private ExtraSamples _extraSamples;
        private ExtraSamples ExtraSamples => _extraSamples ?? (_extraSamples = new ExtraSamples(Directory.GetCurrentDirectory() + "\\SampleLibrary\\Extra"));

        private List<SamplePage> SamplePages => _samplePages ?? (_samplePages = new List<SamplePage>());

        public SamplePage CurrentSamplePage => _samplePages.Count < _selectedPage ? _samplePages[_selectedPage] : null;

        public static SampleLibrary Instance => _instance ?? (_instance = new SampleLibrary());

        private SampleLibrary()
        {
            var directories = Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\SampleLibrary\\Pages");

            foreach (var directory in directories) SamplePages.Add(new SamplePage(directory));
        }
        
        internal void PrevPage()
        {
            _selectedPage--;
            if (_selectedPage < 0) _selectedPage = SamplePages.Count - 1;
        }

        internal void NextPage()
        {
            _selectedPage++;
            if (_selectedPage > SamplePages.Count - 1) _selectedPage = 0;
        }

        internal void NextExtraPage()
        {
            ExtraSamples.NextBatch();
        }

        internal void PrevExtraPage()
        {
            ExtraSamples.PrevBatch();
        }

        internal string GetSamplePath(int buttonValue)
        {
            var value = ButtonValue.FromRaw(buttonValue);
            if (!UseExtra) return CurrentSamplePage?.GetSamplePath(value) ?? "";
            
            UseExtra = !UseExtra;
            return ExtraSamples.GetSamplePath(value);

        }

        public string GetSampleName(SampleIndex index)
        {
            if (UseExtra)
                return ExtraSamples.GetSample(index)?.Name ?? "";
            return CurrentSamplePage?.GetSample(index).Name ?? "";
        }

        internal void ToggleExtraPage()
        {
            UseExtra = !UseExtra;
        }
    }
}