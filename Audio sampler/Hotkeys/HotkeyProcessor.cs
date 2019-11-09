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
    public enum ModKeys
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
        private static HotkeyProcessor _instance;
        private static HotkeyProcessor Instance
        {
            get {
                return _instance;
            }
        }



        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey(
            [In] IntPtr hWnd,
            [In] int id,
            [In] uint fsModifiers,
            [In] uint vk
            );

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey(
            [In] IntPtr hWnd,
            [In] int id
            );

        public string Test
        {
            get {
                return "Проверка";
            }
        }

        private AudioPlayer _player;
        public AudioPlayer Player
        {
            get {
                return _player;
            }
        }

        private GlobalKeyboardHook _globalKeyboardHook;

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            Debug.WriteLine(e.KeyboardData.VirtualCode);

            if (e.KeyboardData.VirtualCode != GlobalKeyboardHook.VkSnapshot)
                return;

            // seems, not needed in the life.
            //if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyDown &&
            //    e.KeyboardData.Flags == GlobalKeyboardHook.LlkhfAltdown)
            //{
            //    MessageBox.Show("Alt + Print Screen");
            //    e.Handled = true;
            //}
            //else

            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                MessageBox.Show("Print Screen");
                e.Handled = true;
            }
        }

        public static HotkeyProcessor GetInstance(WindowInteropHelper helper)
        {
            if (Instance == null)
            {
                if (helper != null)
                {
                    _instance = new HotkeyProcessor(helper);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }

            return Instance;
        }

        private Dictionary<HotkeyButton, HotkeyAction> _hotkeyMap;
        public Dictionary<HotkeyButton, HotkeyAction> HotkeyMap
        {
            get {
                return _hotkeyMap ?? (_hotkeyMap = new Dictionary<HotkeyButton, HotkeyAction>());
            }
        }

        private WindowInteropHelper helper;
        private HwndSource _source;

        public HotkeyProcessor(WindowInteropHelper helper)
        {
            _player = AudioPlayer.GetInstance();
            this.helper = helper;
            Init();
        }

        private void Init()
        {
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(HwndHook);
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
            RegisterHotKey();
        }

        public void Close()
        {
            foreach (KeyValuePair<HotkeyButton, HotkeyAction> hotkey in HotkeyMap)
            {
                UnregisterHotKey(hotkey.Key);
            }
            _globalKeyboardHook?.Dispose();
            _source.RemoveHook(HwndHook);
            _source = null;
        }

        private void RegisterHotKey()
        {
            string[] lines = File.ReadAllLines(Application.StartupPath + "\\hotkeys.txt");

            foreach (String line in lines)
            {
                String[] record = line.Split(' ');

                Keys key = (Keys) Enum.Parse(typeof(Keys), record[0]);

                if (record[1].Contains("PlayPool"))
                {
                    HotkeyButton hotkey_no_mod = new HotkeyButton(key);
                    HotkeyAction action_no_mod = (HotkeyAction) Enum.Parse(typeof(HotkeyAction), record[1]);

                    HotkeyMap.Add(hotkey_no_mod, action_no_mod);

                    HotkeyButton hotkey_mod_ctrl = new HotkeyButton(key, (int) ModKeys.ModCtrl);
                    HotkeyAction action_mod_ctrl = (HotkeyAction) Enum.Parse(typeof(HotkeyAction), record[1] + "_MOD_CTRL");

                    HotkeyMap.Add(hotkey_mod_ctrl, action_mod_ctrl);

                    HotkeyButton hotkey_mod_alt = new HotkeyButton(key, (int) ModKeys.ModAlt);
                    HotkeyAction action_mod_alt = (HotkeyAction) Enum.Parse(typeof(HotkeyAction), record[1] + "_MOD_ALT");

                    HotkeyMap.Add(hotkey_mod_alt, action_mod_alt);

                    HotkeyButton hotkey_mod_ctrl_alt = new HotkeyButton(key, ((int) ModKeys.ModCtrl | (int) ModKeys.ModAlt));
                    HotkeyAction action_mod_ctrl_alt = (HotkeyAction) Enum.Parse(typeof(HotkeyAction), record[1] + "_MOD_CTRL_ALT");

                    HotkeyMap.Add(hotkey_mod_ctrl_alt, action_mod_ctrl_alt);
                }
                else
                {
                    HotkeyButton hotkey = new HotkeyButton(key);
                    HotkeyAction action = (HotkeyAction) Enum.Parse(typeof(HotkeyAction), record[1]);

                    HotkeyMap.Add(hotkey, action);
                }
            }

            foreach (KeyValuePair<HotkeyButton, HotkeyAction> entity in HotkeyMap)
            {
                RegisterHotKey(entity.Key);
            }
        }

        private void RegisterHotKey(HotkeyButton hotkey)
        {
            if (!RegisterHotKey(helper.Handle, hotkey.GetHashCode(), (uint) hotkey.ModKeyCode, (uint) hotkey.KeyCode))
            {
                throw new ExternalException();
            }
        }

        private void UnregisterHotKey(HotkeyButton hotkey)
        {
            UnregisterHotKey(helper.Handle, hotkey.GetHashCode());
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;

            switch (msg)
            {
                case WM_HOTKEY:
                    foreach (KeyValuePair<HotkeyButton, HotkeyAction> hotkey in HotkeyMap)
                    {
                        if (hotkey.Key.GetHashCode().Equals(wParam.ToInt32()))
                        {
                            AudioPlayer.GetInstance().ProcessHotkey(hotkey.Value);
                            handled = true;
                        }
                    }
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
