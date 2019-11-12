using System;
using System.Threading;
using Audio_sampler.Win32;

namespace Audio_sampler.Hotkeys
{
    public class MessageLoop
    {
        private const WindowMessage CustomDestroyWindowMessage = (WindowMessage) 4919;

        private static MessageLoop _instance;

        private readonly object _lock = new object();

        private HotkeyProcessor _hook;
        private bool _isInitialized;

        private bool _isRunning = true;
        private Thread _thread;

        private IntPtr _threadId;

        public static MessageLoop GetInstance()
        {
            return _instance ?? (_instance = new MessageLoop());
        }

        public void Run()
        {
            _thread = new Thread(WindowThread)
            {
                IsBackground = true
            };
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();
            while (!_isInitialized) Thread.Sleep(10);
        }

        public void Join()
        {
            if (_thread.IsAlive) _thread.Join();
        }

        public void Stop()
        {
            _isRunning = false;
            _hook.Close();
            DestroyWindow();
        }

        private void WindowThread()
        {
            _threadId = (IntPtr) User32.GetCurrentThreadId();

            SetupWindow();
            _isInitialized = true;
            Message msg;
            do
            {
                do
                {
                    User32.WaitMessage();
                    msg = new Message();
                } while (!User32.PeekMessageW(ref msg, IntPtr.Zero, 0U, 0U, 1U) && _isRunning);

                if (msg.Msg == CustomDestroyWindowMessage) break;

                User32.TranslateMessage(ref msg);
                User32.DispatchMessageW(ref msg);
            } while (msg.Msg != WindowMessage.Destroy && msg.Msg != WindowMessage.Ncdestroy && _isRunning);
        }

        private void SetupWindow()
        {
            _hook = HotkeyProcessor.GetInstance();
        }

        private void DestroyWindow()
        {
            lock (_lock)
            {
                if (!_isInitialized) throw new InvalidOperationException("OverlayWindow is not initialized");
                User32.PostThreadMessageW(_threadId, CustomDestroyWindowMessage, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}