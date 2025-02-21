using System;
using Silk.NET.SDL;

namespace Infinite1942.Platform;

public static partial class Platform
{
    private static Sdl _sdlHandle;

    private static void InitializeSDL()
    {
        _sdlHandle = Sdl.GetApi();

        _sdlHandle.Init(Sdl.InitEverything);

        // Pass first mouse event when user clicked on window
        _sdlHandle.SetHint(Sdl.HintMouseFocusClickthrough, "1");

        // Don't leave fullscreen on focus loss
        _sdlHandle.SetHint(Sdl.HintVideoMinimizeOnFocusLoss, "0");
    }

    private static void ShutdownSDL()
    {
        _sdlHandle.Quit();
    }

    private static IWindow CreateSDLWindow(string title, int width, int height)
    {
        if (_sdlHandle == null)
        {
            throw new SdlException("SDL is not initialized");
        }

        return new SDLWindow(_sdlHandle, title, width, height);
    }
}
