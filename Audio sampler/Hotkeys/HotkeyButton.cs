using System.Windows.Forms;

namespace Audio_sampler.Hotkeys
{
    internal class HotkeyButton
    {
        public HotkeyButton(Keys keyCode)
        {
            KeyCode = keyCode;
            ModKeyCode = 0;
        }

        public HotkeyButton(Keys keyCode, int modKeyCode)
        {
            KeyCode = keyCode;
            ModKeyCode = modKeyCode;
        }

        private Keys KeyCode { get; }

        private int ModKeyCode { get; }

        public override bool Equals(object obj)
        {
            return obj is HotkeyButton hotkey &&
                   KeyCode == hotkey.KeyCode &&
                   ModKeyCode == hotkey.ModKeyCode;
        }

        public override int GetHashCode()
        {
            var hashCode = 385333320;
            hashCode = hashCode * -1521134295 + KeyCode.GetHashCode();
            hashCode = hashCode * -1521134295 + ModKeyCode.GetHashCode();
            return hashCode;
        }
    }
}