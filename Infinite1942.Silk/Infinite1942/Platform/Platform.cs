using System;

namespace Infinite1942.Platform
{
    public static partial class Platform
    {
        public static void Initialize()
        {
            InitializeSDL();
        }

        public static void Shutdown()
        {
            ShutdownSDL();
        }

        public static IWindow GetWindow(string title, int width, int height)
        {
            return CreateSDLWindow(title, width, height);
        }
    }
}
