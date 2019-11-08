using Audio_sampler.Player;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Diagnostics;
using System.Windows.Input;

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
                FPS = 10,
                X = 1620,
                Y = 500,
                Width = 300,
                Height = 150
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
            SampleLibrary sampleLibrary = AudioPlayer.GetInstance().SampleLibrary;

            string cashTitle = string.Empty;
            string[] cash = new string[9];

            if (sampleLibrary.useExtra)
            {
                cashTitle = "Прочее";

                for (int iter = 0; iter < 9; iter++)
                {
                    if((sampleLibrary.ExtraSamples.sampleCursor + iter) < sampleLibrary.ExtraSamples.Samples.Count)
                    {
                        cash[iter] = sampleLibrary.ExtraSamples.Samples[sampleLibrary.ExtraSamples.sampleCursor + iter].Name;
                    }
                    else
                    {
                        cash[iter] = string.Empty;
                    }
                    
                }
            }
            else
            {
                cashTitle = sampleLibrary.SamplePages[sampleLibrary.selectedPage].Name;

                for (int iter = 0; iter < 9; iter++)
                {
                    if (sampleLibrary.SamplePages[sampleLibrary.selectedPage].Pools.ContainsKey(iter+1))
                    {
                        cash[iter] = sampleLibrary.SamplePages[sampleLibrary.selectedPage].Pools[iter+1].Name;
                    } else
                    {
                        cash[iter] = string.Empty;
                    }                    
                }
            }

            // you do not need to call BeginScene() or EndScene()
            var gfx = e.Graphics;

            gfx.ClearScene(); // set the background of the scene (can be transparent)        
            gfx.FillRoundedRectangle(_backGray, 0, 0, 300, 150, 10);

            gfx.DrawText(_font, 16, _white, 10, 10, cashTitle);

            var i = 0;
            foreach(string cashSampleName in cash)
            {
                gfx.DrawText(_font, 12, _white, 10 + ((i % 2)) * 150, 30 + ((i / 2) + 1) * 20, cashSampleName);
                i++;
            }

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