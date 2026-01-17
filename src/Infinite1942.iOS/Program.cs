using System;
using Foundation;
using Microsoft.Xna.Framework;
using UIKit;

namespace Infinite1942.iOS
{
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static Game game;

        internal static void RunGame()
        {
            game = new Infinite1942Game();
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, typeof(Program));
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
    }
}