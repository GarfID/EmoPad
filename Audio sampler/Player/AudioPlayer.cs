using System;
using System.IO;
using Audio_sampler.Hotkeys;
using NAudio.Wave;

namespace Audio_sampler.Player
{
    public class AudioPlayer
    {
        private static AudioPlayer _instance;
        private readonly WaveOutEvent _waveOut;

        private SampleLibrary _sampleLibrary;

        private AudioPlayer()
        {
            var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\config.txt");

            var devId = int.Parse(lines[0]);
            _waveOut = new WaveOutEvent {DeviceNumber = devId};

            _sampleLibrary = SampleLibrary.Instance;
        }

        private SampleLibrary SampleLibrary => _sampleLibrary ?? (_sampleLibrary = SampleLibrary.Instance);
        public static AudioPlayer Instance => _instance ?? (_instance = new AudioPlayer());

        private void Play(string path)
        {
            if (path.Equals("")) return;
            
            var reader = new Mp3FileReader(path);
            _waveOut.Stop();
            _waveOut.Init(reader);
            _waveOut.Play();
        }

        internal void ProcessHotkey(HotkeyAction value)
        {
            switch (value)
            {
                case HotkeyAction.NextPage:
                    SampleLibrary.NextPage();
                    break;
                case HotkeyAction.PrevPage:
                    SampleLibrary.PrevPage();
                    break;
                case HotkeyAction.ToggleExtraPage:
                    SampleLibrary.ToggleExtraPage();
                    break;
                case HotkeyAction.NextExtraPage:
                    SampleLibrary.NextExtraPage();
                    break;
                case HotkeyAction.PrevExtraPage:
                    SampleLibrary.PrevExtraPage();
                    break;
                case HotkeyAction.PlayPool1:
                    Play(SampleLibrary.GetSamplePath(1));
                    break;
                case HotkeyAction.PlayPool1ModAlt:
                    Play(SampleLibrary.GetSamplePath(1 + 9));
                    break;
                case HotkeyAction.PlayPool1ModCtrl:
                    Play(SampleLibrary.GetSamplePath(1 + 18));
                    break;
                case HotkeyAction.PlayPool1ModCtrlAlt:
                    Play(SampleLibrary.GetSamplePath(1 + 27));
                    break;
                case HotkeyAction.PlayPool2:
                    Play(SampleLibrary.GetSamplePath(2));
                    break;
                case HotkeyAction.PlayPool2ModAlt:
                    Play(SampleLibrary.GetSamplePath(2 + 9));
                    break;
                case HotkeyAction.PlayPool2ModCtrl:
                    Play(SampleLibrary.GetSamplePath(2 + 18));
                    break;
                case HotkeyAction.PlayPool2ModCtrlAlt:
                    Play(SampleLibrary.GetSamplePath(2 + 27));
                    break;
                case HotkeyAction.PlayPool3:
                    Play(SampleLibrary.GetSamplePath(3));
                    break;
                case HotkeyAction.PlayPool3ModAlt:
                    Play(SampleLibrary.GetSamplePath(3 + 9));
                    break;
                case HotkeyAction.PlayPool3ModCtrl:
                    Play(SampleLibrary.GetSamplePath(3 + 18));
                    break;
                case HotkeyAction.PlayPool3ModCtrlAlt:
                    Play(SampleLibrary.GetSamplePath(3 + 27));
                    break;
                case HotkeyAction.PlayPool4:
                    Play(SampleLibrary.GetSamplePath(4));
                    break;
                case HotkeyAction.PlayPool4ModAlt:
                    Play(SampleLibrary.GetSamplePath(4 + 9));
                    break;
                case HotkeyAction.PlayPool4ModCtrl:
                    Play(SampleLibrary.GetSamplePath(4 + 18));
                    break;
                case HotkeyAction.PlayPool4ModCtrlAlt:
                    Play(SampleLibrary.GetSamplePath(4 + 27));
                    break;
                case HotkeyAction.PlayPool5:
                    Play(SampleLibrary.GetSamplePath(5));
                    break;
                case HotkeyAction.PlayPool5ModAlt:
                    Play(SampleLibrary.GetSamplePath(5 + 9));
                    break;
                case HotkeyAction.PlayPool5ModCtrl:
                    Play(SampleLibrary.GetSamplePath(5 + 18));
                    break;
                case HotkeyAction.PlayPool5ModCtrlAlt:
                    Play(SampleLibrary.GetSamplePath(5 + 27));
                    break;
                case HotkeyAction.PlayPool6:
                    Play(SampleLibrary.GetSamplePath(6));
                    break;
                case HotkeyAction.PlayPool6ModAlt:
                    Play(SampleLibrary.GetSamplePath(6 + 9));
                    break;
                case HotkeyAction.PlayPool6ModCtrl:
                    Play(SampleLibrary.GetSamplePath(6 + 18));
                    break;
                case HotkeyAction.PlayPool6ModCtrlAlt:
                    Play(SampleLibrary.GetSamplePath(6 + 27));
                    break;
                case HotkeyAction.PlayPool7:
                    Play(SampleLibrary.GetSamplePath(7));
                    break;
                case HotkeyAction.PlayPool7ModAlt:
                    Play(SampleLibrary.GetSamplePath(7 + 9));
                    break;
                case HotkeyAction.PlayPool7ModCtrl:
                    Play(SampleLibrary.GetSamplePath(7 + 18));
                    break;
                case HotkeyAction.PlayPool7ModCtrlAlt:
                    Play(SampleLibrary.GetSamplePath(7 + 27));
                    break;
                case HotkeyAction.PlayPool8:
                    Play(SampleLibrary.GetSamplePath(8));
                    break;
                case HotkeyAction.PlayPool8ModAlt:
                    Play(SampleLibrary.GetSamplePath(8 + 9));
                    break;
                case HotkeyAction.PlayPool8ModCtrl:
                    Play(SampleLibrary.GetSamplePath(8 + 18));
                    break;
                case HotkeyAction.PlayPool8ModCtrlAlt:
                    Play(SampleLibrary.GetSamplePath(8 + 27));
                    break;
                case HotkeyAction.PlayPool9:
                    Play(SampleLibrary.GetSamplePath(9));
                    break;
                case HotkeyAction.PlayPool9ModAlt:
                    Play(SampleLibrary.GetSamplePath(9 + 9));
                    break;
                case HotkeyAction.PlayPool9ModCtrl:
                    Play(SampleLibrary.GetSamplePath(9 + 18));
                    break;
                case HotkeyAction.PlayPool9ModCtrlAlt:
                    Play(SampleLibrary.GetSamplePath(9 + 27));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}