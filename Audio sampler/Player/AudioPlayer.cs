using Audio_sampler.Hotkeys;
using NAudio.Wave;
using System;
using System.IO;

namespace Audio_sampler.Player
{
    public class AudioPlayer
    {     
        private readonly WaveOutEvent waveOut;

        private SampleLibrary _sampleLibrary;
        public SampleLibrary SampleLibrary => _sampleLibrary ?? (_sampleLibrary = SampleLibrary.Instance);

        private static AudioPlayer _instance;
        public static AudioPlayer Instance => _instance ?? (_instance = new AudioPlayer());

        public AudioPlayer()
        {
            string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\config.txt");

            int devId = Int32.Parse(lines[0]);
            waveOut = new WaveOutEvent() { DeviceNumber = devId };

            _sampleLibrary = SampleLibrary.Instance;
        }

        public void Play(string path)
        {
            if (!path.Equals(string.Empty))
            {
                Mp3FileReader reader = new Mp3FileReader(path);
                waveOut.Stop();
                waveOut.Init(reader);
                waveOut.Play();
            }
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
                case HotkeyAction.PlayPool1_MOD_ALT:
                    Play(SampleLibrary.GetSamplePath(1 + 9));
                    break;
                case HotkeyAction.PlayPool1_MOD_CTRL:
                    Play(SampleLibrary.GetSamplePath(1 + 18));
                    break;
                case HotkeyAction.PlayPool1_MOD_CTRL_ALT:
                    Play(SampleLibrary.GetSamplePath(1 + 27));
                    break;
                case HotkeyAction.PlayPool2:
                    Play(SampleLibrary.GetSamplePath(2));
                    break;
                case HotkeyAction.PlayPool2_MOD_ALT:
                    Play(SampleLibrary.GetSamplePath(2 + 9));
                    break;
                case HotkeyAction.PlayPool2_MOD_CTRL:
                    Play(SampleLibrary.GetSamplePath(2 + 18));
                    break;
                case HotkeyAction.PlayPool2_MOD_CTRL_ALT:
                    Play(SampleLibrary.GetSamplePath(2 + 27));
                    break;
                case HotkeyAction.PlayPool3:
                    Play(SampleLibrary.GetSamplePath(3));
                    break;
                case HotkeyAction.PlayPool3_MOD_ALT:
                    Play(SampleLibrary.GetSamplePath(3 + 9));
                    break;
                case HotkeyAction.PlayPool3_MOD_CTRL:
                    Play(SampleLibrary.GetSamplePath(3 + 18));
                    break;
                case HotkeyAction.PlayPool3_MOD_CTRL_ALT:
                    Play(SampleLibrary.GetSamplePath(3 + 27));
                    break;
                case HotkeyAction.PlayPool4:
                    Play(SampleLibrary.GetSamplePath(4));
                    break;
                case HotkeyAction.PlayPool4_MOD_ALT:
                    Play(SampleLibrary.GetSamplePath(4 + 9));
                    break;
                case HotkeyAction.PlayPool4_MOD_CTRL:
                    Play(SampleLibrary.GetSamplePath(4 + 18));
                    break;
                case HotkeyAction.PlayPool4_MOD_CTRL_ALT:
                    Play(SampleLibrary.GetSamplePath(4 + 27));
                    break;
                case HotkeyAction.PlayPool5:
                    Play(SampleLibrary.GetSamplePath(5));
                    break;
                case HotkeyAction.PlayPool5_MOD_ALT:
                    Play(SampleLibrary.GetSamplePath(5 + 9));
                    break;
                case HotkeyAction.PlayPool5_MOD_CTRL:
                    Play(SampleLibrary.GetSamplePath(5 + 18));
                    break;
                case HotkeyAction.PlayPool5_MOD_CTRL_ALT:
                    Play(SampleLibrary.GetSamplePath(5 + 27));
                    break;
                case HotkeyAction.PlayPool6:
                    Play(SampleLibrary.GetSamplePath(6));
                    break;
                case HotkeyAction.PlayPool6_MOD_ALT:
                    Play(SampleLibrary.GetSamplePath(6 + 9));
                    break;
                case HotkeyAction.PlayPool6_MOD_CTRL:
                    Play(SampleLibrary.GetSamplePath(6 + 18));
                    break;
                case HotkeyAction.PlayPool6_MOD_CTRL_ALT:
                    Play(SampleLibrary.GetSamplePath(6 + 27));
                    break;
                case HotkeyAction.PlayPool7:
                    Play(SampleLibrary.GetSamplePath(7));
                    break;
                case HotkeyAction.PlayPool7_MOD_ALT:
                    Play(SampleLibrary.GetSamplePath(7 + 9));
                    break;
                case HotkeyAction.PlayPool7_MOD_CTRL:
                    Play(SampleLibrary.GetSamplePath(7 + 18));
                    break;
                case HotkeyAction.PlayPool7_MOD_CTRL_ALT:
                    Play(SampleLibrary.GetSamplePath(7 + 27));
                    break;
                case HotkeyAction.PlayPool8:
                    Play(SampleLibrary.GetSamplePath(8));
                    break;
                case HotkeyAction.PlayPool8_MOD_ALT:
                    Play(SampleLibrary.GetSamplePath(8 + 9));
                    break;
                case HotkeyAction.PlayPool8_MOD_CTRL:
                    Play(SampleLibrary.GetSamplePath(8 + 18));
                    break;
                case HotkeyAction.PlayPool8_MOD_CTRL_ALT:
                    Play(SampleLibrary.GetSamplePath(8 + 27));
                    break;
                case HotkeyAction.PlayPool9:
                    Play(SampleLibrary.GetSamplePath(9));
                    break;
                case HotkeyAction.PlayPool9_MOD_ALT:
                    Play(SampleLibrary.GetSamplePath(9 + 9));
                    break;
                case HotkeyAction.PlayPool9_MOD_CTRL:
                    Play(SampleLibrary.GetSamplePath(9 + 18));
                    break;
                case HotkeyAction.PlayPool9_MOD_CTRL_ALT:
                    Play(SampleLibrary.GetSamplePath(9 + 27));
                    break;
            }
        }
    }
}
