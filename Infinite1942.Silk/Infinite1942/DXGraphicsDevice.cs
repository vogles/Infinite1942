using Silk.NET.Core.Native;
using Silk.NET.Direct3D.Compilers;
using Silk.NET.Direct3D11;
using Silk.NET.DXGI;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infinite1942.Graphics
{
    public class DXGraphicsDevice : GraphicsDevice
    {
        // Load the DXGI and Direct3D11 libraries for later use.
        // Given this is not tied to the window, this doesn't need to be done in the OnLoad event.
        private DXGI _dxgi = null!;
        private D3D11 _d3d11 = null!;
        private D3DCompiler _d3dCompiler = null!;

        private ComPtr<ID3D11Device> _d3dDevice = default;
        private ComPtr<IDXGISwapChain1> _d3dSwapchain = default;
        private ComPtr<ID3D11DeviceContext> _d3dDeviceContext = default;

        public DXGraphicsDevice(IWindow window) : base(window)
        {
            //Whether or not to force use of DXVK on platforms where native DirectX implementations are available
            const bool forceDxvk = false;

            _dxgi = DXGI.GetApi(_window, forceDxvk);
            _d3d11 = D3D11.GetApi(_window, forceDxvk);
            _d3dCompiler = D3DCompiler.GetApi();
        }

        public override void Initialize()
        {
            var swapChainDesc = new SwapChainDesc()
            {
                BufferDesc = new ModeDesc()
                {
                    Format = Format.FormatB8G8R8A8Unorm,
                    Height = 0,
                    Width = 0,
                    RefreshRate = new Rational(0, 0),
                    Scaling = ModeScaling.Unspecified,
                    ScanlineOrdering = ModeScanlineOrder.Unspecified
                },
                BufferUsage = DXGI.UsageRenderTargetOutput,
                BufferCount = 2,
                Flags = 0,
                SampleDesc = new SampleDesc(1, 0),
                SwapEffect = SwapEffect.FlipDiscard,
                OutputWindow = _window.Native!.DXHandle!.Value,
                Windowed = true,
            };

            D3DFeatureLevel d3DFeatureLevel = D3DFeatureLevel.Level110;

            SilkMarshal.ThrowHResult(
                _d3d11.CreateDeviceAndSwapChain(
                    pAdapter: default(ComPtr<IDXGIAdapter>),
                    DriverType: D3DDriverType.Hardware,
                    Software: default,
                    Flags: (uint)CreateDeviceFlag.Debug,
                    pFeatureLevels: ref d3DFeatureLevel,
                    FeatureLevels: 0,
                    SDKVersion: (uint)D3D11.SdkVersion,
                    ref swapChainDesc,
                    ref _d3dSwapchain,
                    ref _d3dDevice,
                    ref d3DFeatureLevel,
                    ref _d3dDeviceContext)
            );

            _d3dDevice.SetInfoQueueCallback(msg => {
                unsafe
                {
                    Console.WriteLine($"D3D Device Info Message: {SilkMarshal.PtrToString((nint)msg.PDescription)}");
                }
            });
        }

        public override void Shutdown()
        {
            _d3dDeviceContext.Dispose();
            _d3dSwapchain.Dispose();
            _d3dDevice.Dispose();
        }

        public ComPtr<ID3D11VertexShader> CreateVertexShader(ComPtr<ID3D10Blob> vertexCode)
        {
            ComPtr<ID3D11VertexShader> vertexShader = default;

            unsafe
            {
                SilkMarshal.ThrowHResult(
                    _d3dDevice.CreateVertexShader(
                        vertexCode.GetBufferPointer(),
                        vertexCode.GetBufferSize(),
                        ref Unsafe.NullRef<ID3D11ClassLinkage>(),
                        ref vertexShader)
                );
            }

            return vertexShader;
        }

        public ComPtr<ID3D11PixelShader> CreateFragmentShader(ComPtr<ID3D10Blob> fragmentCode)
        {
            ComPtr<ID3D11PixelShader> fragmentShader = default;

            unsafe
            {
                SilkMarshal.ThrowHResult(
                    _d3dDevice.CreatePixelShader(
                        fragmentCode.GetBufferPointer(),
                        fragmentCode.GetBufferSize(),
                        ref Unsafe.NullRef<ID3D11ClassLinkage>(),
                        ref fragmentShader)
                );
            }

            return fragmentShader;
        }

        public ComPtr<ID3D11InputLayout> CreateInputLayout(ComPtr<ID3D10Blob> vertexCode)
        {
            ComPtr<ID3D11InputLayout> inputLayout = default;

            unsafe
            {
                // Describe the layout of the input data for the shader.
                fixed (byte* name = SilkMarshal.StringToMemory("POS"))
                {
                    var inputElement = new InputElementDesc
                    {
                        SemanticName = name,
                        SemanticIndex = 0,
                        Format = Format.FormatR32G32B32Float,
                        InputSlot = 0,
                        AlignedByteOffset = 0,
                        InputSlotClass = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    };

                    SilkMarshal.ThrowHResult
                    (
                        _d3dDevice.CreateInputLayout
                        (
                            in inputElement,
                            1,
                            vertexCode.GetBufferPointer(),
                            vertexCode.GetBufferSize(),
                            ref inputLayout
                        )
                    );
                }
            }

            return inputLayout;
        }
    }
}
