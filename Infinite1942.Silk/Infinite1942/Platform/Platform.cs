// using System;

using System.Runtime.InteropServices;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace Infinite1942
{
    public static partial class Platform
    {
        public static IWindow CreateWindow(string title, int width, int height)
        {
            return CreateWindow(title, new Vector2D<int>(width, height));
        }

        public static IWindow CreateWindow(string title, Vector2D<int> windowSize)
        {
            var windowOptions = WindowOptions.Default;
            if (GetGraphicsBackend() == "DirectX")
            {
                windowOptions.API = GraphicsAPI.None;
            }

            windowOptions.Title = title;
            windowOptions.Size = windowSize;

            return Window.Create(windowOptions);
        }

        public static string GetGraphicsBackend()
        {
            // At some point I might implement Vulkan and Metal. But for now,
            // I'm just going to return OpenGL for everything that isn't Windows.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "DirectX";
            }
            // else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            // {
            //     return "Vulkan";
            // }
            // else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            // {
            //     return "Metal";
            // }
            else
            {
                return "OpenGL";
            }
        }
    }
}
