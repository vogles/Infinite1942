using Veldrid.StartupUtilities;

namespace Infinite1942
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var windowInfo = new WindowCreateInfo()
            {
                X = 100,
                Y = 100,
                WindowTitle = "Infinite 1942",
                WindowWidth = 1280,
                WindowHeight = 720
            };

            var window = VeldridStartup.CreateWindow(ref windowInfo);

            while (window.Exists)
            {
                window.PumpEvents();
            }
        }
    }
}