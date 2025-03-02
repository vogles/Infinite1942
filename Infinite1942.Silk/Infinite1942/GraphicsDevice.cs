using Silk.NET.Windowing;

namespace Infinite1942.Graphics
{
    public abstract class GraphicsDevice : IDisposable
    {
        protected IWindow _window;

        public GraphicsDevice(IWindow window)
        {
            _window = window;
        }

        public abstract void Initialize();
        public abstract void Shutdown();

        #region IDisposable Implementation

        private bool _isDisposed = false;

        ~GraphicsDevice()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                Shutdown();

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}