using System.Windows.Forms;

namespace Audio_sampler.Hotkeys
{
    class HotkeyButton
    {
        private Keys _keyCode;

        public Keys KeyCode
        {
            get {
                return _keyCode;
            }
        }

        private int _modKeyCode;
        public int ModKeyCode
        {
            get {
                return _modKeyCode;
            }
        }

        public HotkeyButton(Keys keyCode)
        {
            _keyCode = keyCode;
            _modKeyCode = 0;
        }

        public HotkeyButton(Keys keyCode, int modKeyCode)
        {
            _keyCode = keyCode;
            _modKeyCode = modKeyCode;
        }

        public override bool Equals(object obj)
        {

            return obj is HotkeyButton hotkey &&
                   KeyCode == hotkey.KeyCode &&
                   ModKeyCode == hotkey.ModKeyCode;
        }

        public override int GetHashCode()
        {
            int hashCode = 385333320;
            hashCode = hashCode * -1521134295 + KeyCode.GetHashCode();
            hashCode = hashCode * -1521134295 + ModKeyCode.GetHashCode();
            return hashCode;
        }
    }
}
