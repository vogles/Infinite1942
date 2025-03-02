using Silk.NET.Core.Native;
using Silk.NET.Direct3D.Compilers;
using Silk.NET.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infinite1942.Graphics
{
    public class DXShader : Shader
    {
        D3DCompiler _d3dCompiler;
        DXGraphicsDevice _graphicsDevice;
        ComPtr<ID3D11VertexShader> _vertexShader = default;
        ComPtr<ID3D11PixelShader> _fragmentShader = default;

        ComPtr<ID3D10Blob> _vertexCode = default;
        ComPtr<ID3D10Blob> _vertexErrors = default;
        ComPtr<ID3D10Blob> _pixelCode = default;
        ComPtr<ID3D10Blob> _pixelErrors = default;

        public DXShader(DXGraphicsDevice graphicsDevice)
        {
            _d3dCompiler = D3DCompiler.GetApi();

            _graphicsDevice = graphicsDevice;
        }

        public override void Initialize(string vertexShader, string fragmentShader)
        {
            var vertexShaderBytes = Encoding.ASCII.GetBytes(vertexShader);
            var fragmentShaderBytes = Encoding.ASCII.GetBytes(fragmentShader);

            unsafe
            {
                HResult hr = _d3dCompiler.Compile
                (
                    in vertexShaderBytes[0],
                    (nuint)vertexShaderBytes.Length,
                    nameof(vertexShader),
                    null,
                    ref Unsafe.NullRef<ID3DInclude>(),
                    "vs_main",
                    "vs_5_0",
                    0,
                    0,
                    ref _vertexCode,
                    ref _vertexErrors
                );

                // Check for compilation errors.
                if (hr.IsFailure)
                {
                    if (_vertexErrors.Handle is not null)
                    {
                        Console.WriteLine($"D3D Vertex Shader Compilation Error: {SilkMarshal.PtrToString((nint)_vertexErrors.GetBufferPointer())}");
                    }

                    hr.Throw();
                }
            }
            
            unsafe
            {
                HResult hr = _d3dCompiler.Compile(
                    in fragmentShaderBytes[0],
                    (nuint)fragmentShaderBytes.Length,
                    nameof(fragmentShader),
                    null,
                    ref Unsafe.NullRef<ID3DInclude>(),
                    "ps_main",
                    "ps_5_0",
                    0,
                    0,
                    ref _pixelCode,
                    ref _pixelErrors
                );

                // Check for compilation errors.
                if (hr.IsFailure)
                {
                    if (_pixelErrors.Handle is not null)
                    {
                        Console.WriteLine($"D3D Fragment Shader Compilation Error: {SilkMarshal.PtrToString((nint)_pixelErrors.GetBufferPointer())}");
                    }

                    hr.Throw();
                }
            }

            // Create vertex shader.
            _vertexShader = _graphicsDevice.CreateVertexShader(_vertexCode);
            _fragmentShader = _graphicsDevice.CreateFragmentShader(_pixelCode);
        }

        public override void Shutdown()
        {
            _vertexCode.Dispose();
            _vertexErrors.Dispose();
            _pixelCode.Dispose();
            _pixelErrors.Dispose();

            _vertexShader.Dispose();
            _fragmentShader.Dispose();
        }
    }
}
