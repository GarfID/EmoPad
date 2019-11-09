using System;
using System.Runtime.InteropServices;
using System.Threading;

using Audio_sampler.Win32;

using GameOverlay.Windows;

namespace Audio_sampler.Hotkeys
{

    public class MessageLoop
    {

        private const WindowMessage CustomDestroyWindowMessage = (WindowMessage)4919;


        private static MessageLoop _instance;

        public static MessageLoop GetInstance() => _instance ?? (_instance = new MessageLoop());

        private object _lock = new object();

        private bool _isRunning = true;
        private bool _isInitialized;

        private Thread _thread;
        private string _title;
        private string _className;
        private string _menuName;
        private WindowProc _windowProc;
        private IntPtr _windowProcAddress;

        private int _x;
        public int X => _x;

        private int _y;
        public int Y => _x;

        private int _width;
        public int Width => _width;

        private int _height;
        public int Height => _height;


        private IntPtr _handle;

        private HotkeyProcessor _hook;

        public void Run()
        {
            _thread = new Thread(WindowThread)
            {
                IsBackground = true
            };
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();
            while (!_isInitialized)
            {
                Thread.Sleep(10);
            }
        }

        public void Join()
        {
            _thread.Join();
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public event EventHandler<GlobalKeyboardHookEventArgs> KeyboardPressed;

        private void WindowThread()
        {
            SetupWindow();
            _isInitialized = true;
            Message msg;
            do
            {
                do
                {
                    User32.WaitMessage();
                    msg = new Message();
                } while (!User32.PeekMessageW(ref msg, _handle, 0U, 0U, 1U));

                if (msg.Msg == CustomDestroyWindowMessage)
                {
                }

                User32.TranslateMessage(ref msg);
                User32.DispatchMessageW(ref msg);
            } while (msg.Msg != WindowMessage.Destroy && msg.Msg != WindowMessage.Ncdestroy);

            User32.UnregisterClassW(_className, IntPtr.Zero);
        }

        private void SetupWindow()
        {
            if (_title == null)
            {
                _title = WindowHelper.GenerateRandomTitle();
            }

            if (_menuName == null)
            {
                _menuName = WindowHelper.GenerateRandomTitle();
            }

            if (_className == null)
            {
                _className = WindowHelper.GenerateRandomClass();
            }

            _windowProc = WindowProcedure;
            _windowProcAddress = Marshal.GetFunctionPointerForDelegate(_windowProc);
            while (true)
            {
                var windowClassEx = new WindowClassEx
                {
                    Size = WindowClassEx.NativeSize(),
                    Style = 0,
                    WindowProc = _windowProcAddress,
                    ClsExtra = 0,
                    WindowExtra = 0,
                    Instance = IntPtr.Zero,
                    Icon = IntPtr.Zero,
                    Curser = IntPtr.Zero,
                    Background = IntPtr.Zero,
                    MenuName = _menuName,
                    ClassName = _className,
                    IconSm = IntPtr.Zero
                };
                if (User32.RegisterClassExW(ref windowClassEx) != 0)
                {
                    break;
                }

                _className = WindowHelper.GenerateRandomClass();
            }

            ExtendedWindowStyle dwExStyle = 0;
            dwExStyle |= ExtendedWindowStyle.Transparent;
            dwExStyle |= ExtendedWindowStyle.NoActivate;
            dwExStyle |= ExtendedWindowStyle.Topmost;
            WindowStyle dwStyle = 0;
            dwStyle |= WindowStyle.Popup;
            dwStyle |= WindowStyle.Visible;
            _handle = User32.CreateWindowExW(dwExStyle, _className, _title, dwStyle, X, Y, Width,
                                             Height, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            User32.SetLayeredWindowAttributes(_handle, 0U, byte.MaxValue, LayeredWindowAttributes.Alpha);
            User32.UpdateWindow(_handle);

            HotkeyProcessor.Initialize(this._handle);
            this._hook = HotkeyProcessor.GetInstance();
        }

        private void DestroyWindow()
        {
            lock (_lock)
            {
                if (!_isInitialized)
                    throw new InvalidOperationException("OverlayWindow is not initialized");
                User32.PostMessageW(this._handle, CustomDestroyWindowMessage, IntPtr.Zero, IntPtr.Zero);
            }
        }

        private IntPtr WindowProcedure(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                case WindowMessage.Paint:
                case WindowMessage.NcPaint:

                    return IntPtr.Zero;
                case WindowMessage.EraseBackground:
                    User32.SendMessageW(hwnd, WindowMessage.Paint, IntPtr.Zero, IntPtr.Zero);

                    break;
                case WindowMessage.Keyup:
                case WindowMessage.Keydown:
                case WindowMessage.Syskeydown:
                case WindowMessage.Syskeyup:
                case WindowMessage.Syscommand:
                    Console.WriteLine($"Test: msg={msg}");

                    return IntPtr.Zero;
                case WindowMessage.DpiChanged:

                    return IntPtr.Zero;
            }

            return User32.DefWindowProcW(hwnd, msg, wParam, lParam);
        }

        private void OnKeyboardPressed(GlobalKeyboardHookEventArgs args)
        {
            KeyboardPressed?.Invoke(this, args);
        }

    }

}