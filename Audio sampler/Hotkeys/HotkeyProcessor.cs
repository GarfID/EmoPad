using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Audio_sampler.Player;

namespace Audio_sampler.Hotkeys
{
    [Flags]
    public enum ModKeys : uint
    {
        ModAlt = 0x0001,
        ModCtrl = 0x0002
    }

    public enum HotkeyAction
    {
        NextPage,
        PrevPage,
        ToggleExtraPage,
        NextExtraPage,
        PrevExtraPage,
        PlayPool1,
        PlayPool1ModCtrl,
        PlayPool1ModAlt,
        PlayPool1ModCtrlAlt,
        PlayPool2,
        PlayPool2ModCtrl,
        PlayPool2ModAlt,
        PlayPool2ModCtrlAlt,
        PlayPool3,
        PlayPool3ModCtrl,
        PlayPool3ModAlt,
        PlayPool3ModCtrlAlt,
        PlayPool4,
        PlayPool4ModCtrl,
        PlayPool4ModAlt,
        PlayPool4ModCtrlAlt,
        PlayPool5,
        PlayPool5ModCtrl,
        PlayPool5ModAlt,
        PlayPool5ModCtrlAlt,
        PlayPool6,
        PlayPool6ModCtrl,
        PlayPool6ModAlt,
        PlayPool6ModCtrlAlt,
        PlayPool7,
        PlayPool7ModCtrl,
        PlayPool7ModAlt,
        PlayPool7ModCtrlAlt,
        PlayPool8,
        PlayPool8ModCtrl,
        PlayPool8ModAlt,
        PlayPool8ModCtrlAlt,
        PlayPool9,
        PlayPool9ModCtrl,
        PlayPool9ModAlt,
        PlayPool9ModCtrlAlt
    }

    internal class HotkeyProcessor
    {
        public static bool ModKeyCtrlDown;
        public static bool ModKeyAltDown;

        private GlobalKeyboardHook _globalKeyboardHook;

        private Dictionary<HotkeyButton, HotkeyAction> _hotkeyMap;

        private HotkeyProcessor()
        {
            Init();
        }

        private static HotkeyProcessor Instance { get; set; }

        private Dictionary<HotkeyButton, HotkeyAction> HotkeyMap =>
            _hotkeyMap ?? (_hotkeyMap = new Dictionary<HotkeyButton, HotkeyAction>());

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            const int ctrlKeyCode = 162;
            const int altKeyCode = 164;

            var keyCode = e.KeyboardData.VirtualCode;
            if (keyCode == ctrlKeyCode || keyCode == altKeyCode)
            {
                if (
                    !ModKeyAltDown &&
                    keyCode == altKeyCode &&
                    (
                        e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyDown ||
                        e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown
                    )
                )
                    ModKeyAltDown = true;

                if (
                    !ModKeyCtrlDown &&
                    keyCode == ctrlKeyCode &&
                    (
                        e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyDown ||
                        e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown
                    )
                )
                    ModKeyCtrlDown = true;

                if (
                    ModKeyAltDown &&
                    keyCode == altKeyCode &&
                    (
                        e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyUp ||
                        e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp
                    )
                )
                    ModKeyAltDown = false;

                if (
                    ModKeyCtrlDown &&
                    keyCode == ctrlKeyCode &&
                    (
                        e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyUp ||
                        e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp
                    )
                )
                    ModKeyCtrlDown = false;
            }
            else
            {
                var tModCtrl = ModKeyCtrlDown ? ModKeys.ModCtrl : 0;
                var tModAlt = ModKeyAltDown ? ModKeys.ModAlt : 0;
                var modKey = tModCtrl | tModAlt;

                Debug.WriteLine(keyCode);
                var hotkey = new HotkeyButton((Keys) keyCode, (int) modKey);

                if (!HotkeyMap.ContainsKey(hotkey)) return;
                
                if (
                    e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyDown ||
                    e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp
                )
                    AudioPlayer.Instance.ProcessHotkey(HotkeyMap[hotkey]);

                e.Handled = true;
            }
        }

        public static HotkeyProcessor GetInstance()
        {
            return Instance ?? (Instance = new HotkeyProcessor());
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
            var lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\hotkeys.txt");

            foreach (var line in lines)
            {
                var record = line.Split(' ');

                var key = (Keys) Enum.Parse(typeof(Keys), record[0]);

                if (record[1].Contains("PlayPool"))
                {
                    var hotkeyNoMod = new HotkeyButton(key);
                    var actionNoMod = (HotkeyAction) Enum.Parse(typeof(HotkeyAction), record[1]);

                    HotkeyMap.Add(hotkeyNoMod, actionNoMod);

                    var hotkeyModCtrl = new HotkeyButton(key, (int) ModKeys.ModCtrl);
                    var actionModCtrl = (HotkeyAction) Enum.Parse(typeof(HotkeyAction), record[1] + "ModCtrl");

                    HotkeyMap.Add(hotkeyModCtrl, actionModCtrl);

                    var hotkeyModAlt = new HotkeyButton(key, (int) ModKeys.ModAlt);
                    var actionModAlt = (HotkeyAction) Enum.Parse(typeof(HotkeyAction), record[1] + "ModAlt");

                    HotkeyMap.Add(hotkeyModAlt, actionModAlt);

                    var hotkeyModCtrlAlt = new HotkeyButton(key, (int) ModKeys.ModCtrl | (int) ModKeys.ModAlt);
                    var actionModCtrlAlt =
                        (HotkeyAction) Enum.Parse(typeof(HotkeyAction), record[1] + "ModCtrlAlt");

                    HotkeyMap.Add(hotkeyModCtrlAlt, actionModCtrlAlt);
                }
                else
                {
                    var hotkey = new HotkeyButton(key);
                    var action = (HotkeyAction) Enum.Parse(typeof(HotkeyAction), record[1]);

                    HotkeyMap.Add(hotkey, action);
                }
            }
        }
    }
}