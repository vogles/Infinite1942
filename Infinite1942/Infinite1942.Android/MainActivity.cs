using Android.Views;
using Microsoft.Xna.Framework;

namespace Infinite1942.Android
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AndroidGameActivity
    {
        private Game _game = null;
        private View _view = null;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _game = new Infinite1942Game();
            _view = _game.Services.GetService<View>();

            // Set our view from the "main" layout resource
            SetContentView(_view);

            _game.Run();
        }
    }
}