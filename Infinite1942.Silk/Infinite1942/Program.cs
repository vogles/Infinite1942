using Silk.NET.SDL;

namespace Infinite1942;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        SdlProvider.InitFlags = Sdl.InitVideo | Sdl.InitEvents;
        var sdl = SdlProvider.SDL.Value;

        unsafe
        {
            var errorString = sdl.GetErrorS();
            if (!String.IsNullOrEmpty(errorString))
            {
                Console.WriteLine($"SDL failed to initialize: {errorString}");
                return;
            }
            
            var window = sdl.CreateWindow("Infinite 1942", Sdl.WindowposCentered, Sdl.WindowposCentered, 800, 600, 0);

            if (window == null)
            {
                errorString = sdl.GetErrorS();
                Console.WriteLine($"SDL CreateWindow failed: {errorString}");
                sdl.Quit();
                return;
            }

            var renderer = sdl.CreateRenderer(window, -1, 0);

            if (renderer == null)
            {
                errorString = sdl.GetErrorS();
                Console.WriteLine($"SDL CreateRenderer failed: {errorString}");
                sdl.DestroyWindow(window);
                sdl.Quit();
                return;
            }

            sdl.SetRenderDrawColor(renderer, 255, 0, 255, 255);

            bool isRunning = true;
            Event evt = new Event();

            while (isRunning)
            {
                while (sdl.PollEvent(ref evt) != 0)
                {
                    if (evt.Type == (uint)EventType.Quit)
                    {
                        isRunning = false;
                    }
                }

                sdl.RenderClear(renderer);
                sdl.RenderPresent(renderer);
            }
            
            sdl.DestroyRenderer(renderer);
            sdl.DestroyWindow(window);
            sdl.Quit();
        }
    }
}