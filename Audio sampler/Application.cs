using Audio_sampler.Hotkeys;
using GameOverlayExample.Examples;
using System;

namespace Audio_sampler
{
    public class Application
    {
        public static Random random = new Random();

        public static void Main()
        {
            GraphicsWindowExample graphicsWindowExample = new GraphicsWindowExample();

            graphicsWindowExample.Initialize();
            graphicsWindowExample.Run();
            graphicsWindowExample.Join();

            HotkeyProcessor.GetInstance().Close();
        }
    }
}