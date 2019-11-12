using System;
using System.Drawing;
using System.Windows.Forms;
using Audio_sampler.Hotkeys;

namespace Audio_sampler
{
    public static class App
    {
        public static readonly Random Random = new Random();

        [STAThread]
        public static void Main()
        {
            Overlay.Overlay.Instance.Run();
            MessageLoop.GetInstance().Run();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayForm());

            Overlay.Overlay.Instance.Join();
            MessageLoop.GetInstance().Join();

            HotkeyProcessor.GetInstance().Close();
        }
    }

    public class TrayForm : ApplicationContext
    {
        private readonly NotifyIcon _trayIcon;

        public TrayForm()
        {
            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            _trayIcon = new NotifyIcon
            {
                Text = @"MyTrayApp",
                Icon = new Icon(SystemIcons.Application, 40, 40),
                ContextMenu = new ContextMenu(new[]
                {
                    new MenuItem("Exit", Exit)
                }),
                Visible = true
            };
        }

        private void Exit(object sender, EventArgs eventArgs)
        {
            Overlay.Overlay.Instance.Stop();
            MessageLoop.GetInstance().Stop();
            _trayIcon.Visible = false;
            Application.Exit();
        }
    }
}