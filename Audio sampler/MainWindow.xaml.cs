using Audio_sampler.Hotkeys;
using GameOverlayExample.Examples;
using System;
using System.Windows;
using System.Windows.Interop;

namespace Audio_sampler
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Random random = new Random();

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            DataContext = HotkeyProcessor.GetInstance(new WindowInteropHelper(this));
        }

        protected override void OnClosed(EventArgs e)
        {
            HotkeyProcessor.GetInstance(null).Close();
            base.OnClosed(e);
        }

        public MainWindow()
        {
            InitializeComponent();

            GraphicsWindowExample graphicsWindowExample = new GraphicsWindowExample();

            ASDF.GetFonts();

            graphicsWindowExample.Initialize();
            graphicsWindowExample.Run();
        }
    }
}
