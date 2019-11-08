using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;

namespace GameOverlayExample.Examples
{
    public class GraphicsWindowExample
    {
        private readonly GraphicsWindow _window;

        private Font _font;

        private SolidBrush _black;
        private SolidBrush _gray;
        private SolidBrush _red;
        private SolidBrush _green;
        private SolidBrush _blue;
        private SolidBrush _white;
        private SolidBrush _backGray;

        public GraphicsWindowExample()
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
                FPS = 60,
                X = 0,
                Y = 0,
                Width = 200,
                Height = 200
            };

            _window.SetupGraphics += _window_SetupGraphics;
            _window.DestroyGraphics += _window_DestroyGraphics;
            _window.DrawGraphics += _window_DrawGraphics;
        }

        ~GraphicsWindowExample()
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

        private void _window_SetupGraphics(object sender, SetupGraphicsEventArgs e)
        {

            Graphics gfx = e.Graphics;

            // creates a simple font with no additional style

            _font = gfx.CreateFont("Century Gothic", 16);

            // colors for brushes will be automatically normalized. 0.0f - 1.0f and 0.0f - 255.0f is accepted!
            _black = gfx.CreateSolidBrush(0, 0, 0);
            _gray = gfx.CreateSolidBrush(0x24, 0x29, 0x2E);

            _red = gfx.CreateSolidBrush(Color.Red); // those are the only pre defined Colors
            _green = gfx.CreateSolidBrush(Color.Green);
            _blue = gfx.CreateSolidBrush(Color.Blue);
            _white = gfx.CreateSolidBrush(new Color(255, 255, 255, 255));
            _backGray = gfx.CreateSolidBrush(new Color(74, 74, 74, 170));
        }

        private void _window_DrawGraphics(object sender, DrawGraphicsEventArgs e)
        {

            // you do not need to call BeginScene() or EndScene()
            var gfx = e.Graphics;

            gfx.ClearScene(); // set the background of the scene (can be transparent)        
            gfx.FillRoundedRectangle(_backGray, 0, 0, 200, 200, 10);

            gfx.DrawText(_font, 16, _white, 20, 20, "test");

        }

        private void _window_DestroyGraphics(object sender, DestroyGraphicsEventArgs e)
        {
            _font.Dispose();

            _black.Dispose();
            _gray.Dispose();
            _red.Dispose();
            _green.Dispose();
            _blue.Dispose();
        }
    }
}