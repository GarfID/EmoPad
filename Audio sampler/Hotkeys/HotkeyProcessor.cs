using Audio_sampler.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Audio_sampler.Hotkeys
{
    [Flags]
    public enum ModKeys : uint
    {
        ModAlt = 0x0001,
        ModCtrl = 0x0002,
        ModShift = 0x0003,
        ModWin = 0x0004
    }

    public enum HotkeyAction
    {
        NextPage,
        PrevPage,
        ToggleExtraPage,
        NextExtraPage,
        PrevExtraPage,
        PlayPool1,
        PlayPool1_MOD_CTRL,
        PlayPool1_MOD_ALT,
        PlayPool1_MOD_CTRL_ALT,
        PlayPool2,
        PlayPool2_MOD_CTRL,
        PlayPool2_MOD_ALT,
        PlayPool2_MOD_CTRL_ALT,
        PlayPool3,
        PlayPool3_MOD_CTRL,
        PlayPool3_MOD_ALT,
        PlayPool3_MOD_CTRL_ALT,
        PlayPool4,
        PlayPool4_MOD_CTRL,
        PlayPool4_MOD_ALT,
        PlayPool4_MOD_CTRL_ALT,
        PlayPool5,
        PlayPool5_MOD_CTRL,
        PlayPool5_MOD_ALT,
        PlayPool5_MOD_CTRL_ALT,
        PlayPool6,
        PlayPool6_MOD_CTRL,
        PlayPool6_MOD_ALT,
        PlayPool6_MOD_CTRL_ALT,
        PlayPool7,
        PlayPool7_MOD_CTRL,
        PlayPool7_MOD_ALT,
        PlayPool7_MOD_CTRL_ALT,
        PlayPool8,
        PlayPool8_MOD_CTRL,
        PlayPool8_MOD_ALT,
        PlayPool8_MOD_CTRL_ALT,
        PlayPool9,
        PlayPool9_MOD_CTRL,
        PlayPool9_MOD_ALT,
        PlayPool9_MOD_CTRL_ALT,
    }

    class HotkeyProcessor
    {
        public static bool ModKeyCtrlDown = false;
        public static bool ModKeyAltDown = false;

        private static HotkeyProcessor _instance;
        private static HotkeyProcessor Instance => _instance;

        private AudioPlayer _player;
        public AudioPlayer Player => _player;

        private GlobalKeyboardHook _globalKeyboardHook;

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            int ctrlKeyCode = 162;
            int altKeyCode = 164;

            int keyCode = e.KeyboardData.VirtualCode;
            if (keyCode == ctrlKeyCode || keyCode == altKeyCode)
            {
                if (
                    !HotkeyProcessor.ModKeyAltDown &&
                    keyCode == altKeyCode &&
                        (
                            e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyDown ||
                            e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown
                        )
                    )
                {
                    HotkeyProcessor.ModKeyAltDown = true;
                }

                if (
                    !HotkeyProcessor.ModKeyCtrlDown &&
                    keyCode == ctrlKeyCode &&
                        (
                            e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyDown ||
                            e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown
                        )
                    )
                {
                    HotkeyProcessor.ModKeyCtrlDown = true;
                }

                if (
                    HotkeyProcessor.ModKeyAltDown &&
                    keyCode == altKeyCode &&
                        (
                            e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyUp ||
                            e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp
                        )
                    )
                {
                    HotkeyProcessor.ModKeyAltDown = false;
                }

                if (
                    HotkeyProcessor.ModKeyCtrlDown &&
                    keyCode == ctrlKeyCode &&
                        (
                            e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyUp ||
                            e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp
                        )
                    )
                {
                    HotkeyProcessor.ModKeyCtrlDown = false;
                }
            }
            else
            {

                ModKeys t_modCtrl = HotkeyProcessor.ModKeyCtrlDown ? ModKeys.ModCtrl : 0;
                ModKeys t_modAlt = HotkeyProcessor.ModKeyAltDown ? ModKeys.ModAlt : 0;
                ModKeys modKey = t_modCtrl | t_modAlt;

                Debug.WriteLine(keyCode);
                HotkeyButton hotkey = new HotkeyButton((Keys)keyCode, (int)modKey);

                if (HotkeyMap.ContainsKey(hotkey))
                {
                    AudioPlayer.GetInstance().ProcessHotkey(HotkeyMap[hotkey]);
                    e.Handled = true;
                }
            }
        }

        public static HotkeyProcessor GetInstance()
        {
            if (Instance == null)
            {
                _instance = new HotkeyProcessor();
            }

            return Instance;
        }

        private Dictionary<HotkeyButton, HotkeyAction> _hotkeyMap;
        public Dictionary<HotkeyButton, HotkeyAction> HotkeyMap
        {
            get
            {
                return _hotkeyMap ?? (_hotkeyMap = new Dictionary<HotkeyButton, HotkeyAction>());
            }
        }

        public HotkeyProcessor()
        {
            _player = AudioPlayer.GetInstance();
            Init();
        }

        private void Init()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
            RegisterHotKey();
        }

        public void Close()
        {
            _globalKeyboardHook?.Dispose();
        }

        private void RegisterHotKey()
        {
            string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\hotkeys.txt");

            foreach (String line in lines)
            {
                String[] record = line.Split(' ');

                Keys key = (Keys)Enum.Parse(typeof(Keys), record[0]);

                if (record[1].Contains("PlayPool"))
                {
                    HotkeyButton hotkey_no_mod = new HotkeyButton(key);
                    HotkeyAction action_no_mod = (HotkeyAction)Enum.Parse(typeof(HotkeyAction), record[1]);

                    HotkeyMap.Add(hotkey_no_mod, action_no_mod);

                    HotkeyButton hotkey_mod_ctrl = new HotkeyButton(key, (int)ModKeys.ModCtrl);
                    HotkeyAction action_mod_ctrl = (HotkeyAction)Enum.Parse(typeof(HotkeyAction), record[1] + "_MOD_CTRL");

                    HotkeyMap.Add(hotkey_mod_ctrl, action_mod_ctrl);

                    HotkeyButton hotkey_mod_alt = new HotkeyButton(key, (int)ModKeys.ModAlt);
                    HotkeyAction action_mod_alt = (HotkeyAction)Enum.Parse(typeof(HotkeyAction), record[1] + "_MOD_ALT");

                    HotkeyMap.Add(hotkey_mod_alt, action_mod_alt);

                    HotkeyButton hotkey_mod_ctrl_alt = new HotkeyButton(key, ((int)ModKeys.ModCtrl | (int)ModKeys.ModAlt));
                    HotkeyAction action_mod_ctrl_alt = (HotkeyAction)Enum.Parse(typeof(HotkeyAction), record[1] + "_MOD_CTRL_ALT");

                    HotkeyMap.Add(hotkey_mod_ctrl_alt, action_mod_ctrl_alt);
                }
                else
                {
                    HotkeyButton hotkey = new HotkeyButton(key);
                    HotkeyAction action = (HotkeyAction)Enum.Parse(typeof(HotkeyAction), record[1]);

                    HotkeyMap.Add(hotkey, action);
                }
            }
        }
    }
}
