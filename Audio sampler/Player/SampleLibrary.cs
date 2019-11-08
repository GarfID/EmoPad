using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Audio_sampler.Player
{
    public class SampleLibrary
    {
        private static SampleLibrary instance = null;

        private int selectedPage;
        private bool useExtra = false;

        private ExtraSamples _extraSamples;
        public ExtraSamples ExtraSamples
        {
            get {
                return _extraSamples;
            }
        }

        private List<SamplePage> _samplePages;
        public List<SamplePage> SamplePages
        {
            get {
                return _samplePages ?? (_samplePages = new List<SamplePage>());
            }
        }


        public static SampleLibrary GetInstance()
        {
            if (instance == null)
            {
                instance = new SampleLibrary();
            }

            return instance;
        }

        public SampleLibrary()
        {
            string[] directories = Directory.GetDirectories(Application.StartupPath + "\\SampleLibrary\\Pages");

            foreach (string directory in directories)
            {
                SamplePages.Add(new SamplePage(directory));
            }

            _extraSamples = new ExtraSamples(Application.StartupPath + "\\SampleLibrary\\Extra");
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

        internal string GetSamplePath(int index)
        {
            if (useExtra)
            {
                useExtra = !useExtra;
                return ExtraSamples.GetSamplePath(index);
            }
            else
            {
                return SamplePages[selectedPage].GetSamplePath(index);
            }

        }

        internal void ToggleExtraPage()
        {
            useExtra = !useExtra;
        }
    }
}
