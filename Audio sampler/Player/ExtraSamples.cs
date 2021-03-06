﻿using System.Collections.Generic;
using System.IO;

namespace Audio_sampler.Player
{
    public class ExtraSamples
    {
        private int _samplePage;

        private readonly List<Sample> _samples = new List<Sample>();

        public ExtraSamples(string root)
        {
            var files = Directory.GetFiles(root);

            foreach (var file in files) _samples.Add(new Sample(file));
        }

        private int PageCount => (_samples.Count + 1) / Indexes.PageSize;

        private int PageOffset => _samplePage * Indexes.PageSize;

        public void NextBatch()
        {
            var newValue = _samplePage + 1;
            _samplePage = newValue < PageCount ? newValue : 0;
        }

        public void PrevBatch()
        {
            var newValue = _samplePage - 1;
            _samplePage = newValue >= 0 ? newValue : 0;
        }

        private Sample GetSample(ButtonValue value)
        {
            return GetSample(value.SampleIndex);
        }

        internal Sample GetSample(SampleIndex index)
        {
            var actualIndex = PageOffset + index.RawIndex;

            return actualIndex < _samples.Count ? _samples[actualIndex] : null;
        }

        internal string GetSamplePath(ButtonValue value)
        {
            return GetSample(value)?.Path ?? string.Empty;
        }
    }
}