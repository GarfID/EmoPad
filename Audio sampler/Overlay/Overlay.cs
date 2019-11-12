using Audio_sampler.Hotkeys;
using Audio_sampler.Player;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace GameOverlayExample.Examples
{
    public class Overlay
    {
        private static Overlay _instance;
        public static Overlay Instance => _instance ?? (_instance = new Overlay());

        private readonly GraphicsWindow _window;

        private Font _font;

        private SolidBrush _white;
        private SolidBrush _backGray;

        public Overlay()
        {
            // initialize a new Graphics object
            // GraphicsWindow will do the remaining initialization
            var graphics = new Graphics {
                MeasureFPS = false,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                UseMultiThreadedFactories = false,
                VSync = false,
                WindowHandle = IntPtr.Zero
            };

            // it is important to set the window to visible (and topmost) if you want to see it!
            _window = new GraphicsWindow(graphics) {
                IsTopmost = true,
                IsVisible = true,
                FPS = 10,
                X = 1620,
                Y = 500,
                Width = 300,
                Height = 150
            };

            _window.SetupGraphics += Window_SetupGraphics;
            _window.DestroyGraphics += Window_DestroyGraphics;
            _window.DrawGraphics += Window_DrawGraphics;
        }

        ~Overlay()
        {
            // you do not need to dispose the Graphics surface
            _window.Dispose();
        }

        public void Initialize() { }

        public void Run()
        {
            // creates the window and setups the graphics
            _window.StartThread();
        }

        public void Join()
        {
            if (_window.IsRunning)
            {
                _window.JoinGraphicsThread();
                _window.JoinWindowThread();
            }
        }

        public void Stop()
        {
            _window.StopThreadAsync();
        }

        private void Window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {
            Graphics gfx = e.Graphics;
            _font = gfx.CreateFont("Century Gothic", 16);
            _white = gfx.CreateSolidBrush(new Color(255, 255, 255, 255));
            _backGray = gfx.CreateSolidBrush(new Color(74, 74, 74, 170));
        }

        private void Window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {
            DrawScene(e.Graphics);
        }

        private void DrawScene(Graphics gfx)
        {
            var modShift = GetModShift();
            PopulateCash(modShift);

            gfx.ClearScene(); // set the background of the scene (can be transparent)        
            gfx.FillRoundedRectangle(_backGray, 0, 0, 300, 150, 10);

            gfx.DrawText(_font, 16, _white, 10, 10, GetCashTitle());

            for (int i = 0; i < _cash.Length; ++i)
            {
                var name = _cash[i];
                var x = 10 + (i % 2) * 150 + (i / 8) * 75;
                var y = 30 + (i / 2 + 1) * 20;
                gfx.DrawText(_font, 12, _white, x, y, name);
            }
        }

        private int GetModShift()
        {
            int modShift = 0;

            if (HotkeyProcessor.ModKeyAltDown)
            {
                modShift += 9;
            }

            if (HotkeyProcessor.ModKeyCtrlDown)
            {
                modShift += 18;
            }

            return modShift;
        }

        private void PopulateCash(int modShift)
        {
            for (int iter = 0; iter < _cash.Length; ++iter)
            {
                var name = _sampleLibrary.GetSampleName(SampleIndex.First + modShift + iter);
                _cash[iter] = name != "" ? $"{iter + 1}. {name}" : "";
            }
        }

        private string GetCashTitle()
        {
            if (_sampleLibrary.useExtra)
            {
                return "Прочее";
            }
            else
            {
                return _sampleLibrary.CurrentSamplePage.Name;
            }
        }

        private void Window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
        {
            _font.Dispose();

            _white.Dispose();
            _backGray.Dispose();
        }
    }
}