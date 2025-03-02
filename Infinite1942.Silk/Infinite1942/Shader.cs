using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite1942.Graphics
{
    public abstract class Shader : IDisposable
    {
        private bool disposedValue;

        public abstract void Initialize(string vertexShader, string fragmentShader);
        public abstract void Shutdown();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Shutdown();

                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~Shader()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
