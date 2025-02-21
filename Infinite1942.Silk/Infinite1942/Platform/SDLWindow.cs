using Silk.NET.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite1942
{
    public unsafe class SDLWindow : IWindow, IDisposable
    {
        private static Sdl _sdlHandle;
        private Window* _sdlWindow;

        #region Initialization

        public SDLWindow(Sdl sdlHandle, string title, int width, int height)
        {
            _sdlHandle = sdlHandle;

            WindowFlags flags = WindowFlags.AllowHighdpi;

            flags |= WindowFlags.Opengl;

            flags |= /*WindowFlags.Hidden | */WindowFlags.Resizable;

            _sdlWindow = _sdlHandle.CreateWindow(title, Sdl.WindowposCentered, Sdl.WindowposCentered, width, height, (uint)flags);

            if (_sdlWindow == null)
            {
                throw new Exception($"Cannot allocate SDL Window: {_sdlHandle.GetErrorS()}");
            }


        }
        #endregion

        #region IDisposable
        private bool _isDisposed = false;

        public event EventHandler Disposed;
        
        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~SDLWindow()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    Disposed?.Invoke(this, EventArgs.Empty);
                }

                _sdlHandle.DestroyWindow(_sdlWindow);
                _sdlWindow = null;
                _isDisposed = true;
            }
        }
        #endregion
    }
}
