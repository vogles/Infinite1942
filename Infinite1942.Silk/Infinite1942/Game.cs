using System;
using System.Drawing;
using Silk.NET.Core.Contexts;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Infinite1942
{
    public partial class Game
    {
        private IWindow _window = null;
        private GL _gl = null;
        private Vector2D<int> _windowSize;
        private string _windowTitle;
        private bool _isRunning;

        

        public Game() : this("Game", 800, 600) { }

        public Game(string title, int width, int height)
        {
            _windowTitle = title;
            _windowSize = new Vector2D<int>(width, height);
        }

        private void OnWindowClosing()
        {
            _isRunning = false;
        }
    }
}
