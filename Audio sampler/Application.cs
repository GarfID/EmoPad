using Audio_sampler.Hotkeys;
using Audio_sampler.Properties;
using GameOverlayExample.Examples;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Audio_sampler
{
    public class App
    {
        public static Random random = new Random();

        [STAThread]
        public static void Main()
        {
            Overlay.Instance.Initialize();
            Overlay.Instance.Run();
            MessageLoop.GetInstance().Run();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayForm());

            Overlay.Instance.Join();
            MessageLoop.GetInstance().Join();

            HotkeyProcessor.GetInstance().Close();
        }
    }

    public class TrayForm : ApplicationContext
    {

        private readonly NotifyIcon trayIcon;

        public TrayForm()
        {
            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon
            {
                Text = "MyTrayApp",
                Icon = new Icon(SystemIcons.Application, 40, 40),
                ContextMenu = new ContextMenu(new MenuItem[] {
          new MenuItem("Exit", Exit)
        }),
                Visible = true,
            };
        }

        private void Exit(object sender, EventArgs eventArgs)
        {
            Overlay.Instance.Stop();
            MessageLoop.GetInstance().Stop();
            trayIcon.Visible = false;
            Application.Exit();
        }

    }
}