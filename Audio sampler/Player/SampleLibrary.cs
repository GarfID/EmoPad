using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Audio_sampler.Player
{
    public class SampleLibrary
    {
        private static SampleLibrary _instance;

        public int selectedPage;
        public bool useExtra = false;

        private readonly ExtraSamples _extraSamples;
        public ExtraSamples ExtraSamples => _extraSamples;

        private List<SamplePage> _samplePages;
        public List<SamplePage> SamplePages => _samplePages ?? (_samplePages = new List<SamplePage>());
        public SamplePage CurrentSamplePage => _samplePages[selectedPage];

        public static SampleLibrary Instance => _instance ?? (_instance = new SampleLibrary());

        public SampleLibrary()
        {
            string[] directories = Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\SampleLibrary\\Pages");

            foreach (string directory in directories)
            {
                SamplePages.Add(new SamplePage(directory));
            }

            _extraSamples = new ExtraSamples(Directory.GetCurrentDirectory() + "\\SampleLibrary\\Extra");
        }

        internal void PrevPage()
        {
            selectedPage--;
            if (selectedPage < 0)
            {
                selectedPage = SamplePages.Count - 1;
            }
        }

        internal void NextPage()
        {
            selectedPage++;
            if (selectedPage > SamplePages.Count - 1)
            {
                selectedPage = 0;
            }
        }

        internal void NextExtraPage()
        {
            _extraSamples.NextBatch();
        }

        internal void PrevExtraPage()
        {
            _extraSamples.PrevBatch();
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
            useExtra = !useExtra;
        }
    }
}
