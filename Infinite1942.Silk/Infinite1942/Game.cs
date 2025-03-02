using Infinite1942.Graphics;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace Infinite1942
{
    public partial class Game
    {
        private IWindow _window = null;
        private Vector2D<int> _windowSize;
        private string _windowTitle;
        private bool _isRunning;

        private GraphicsDevice _graphicsDevice;
        private MeshRenderer _meshRenderer;
        private Model _model;
        private Shader _shader;

        public Game() : this("Game", 800, 600) { }

        public Game(string title, int width, int height)
        {
            _windowTitle = title;
            _windowSize = new Vector2D<int>(width, height);
        }

        private void Initialize()
        {
            // Create our vertex buffer.
            List<Vector3D<float>> vertices = new List<Vector3D<float>>()
            {
                //                    X      Y      Z
                new Vector3D<float>( 0.5f,  0.5f,  0.0f),
                new Vector3D<float>( 0.5f, -0.5f,  0.0f),
                new Vector3D<float>(-0.5f, -0.5f,  0.0f),
                new Vector3D<float>(-0.5f,  0.5f,  0.5f),
            };

            // Create our index buffer.
            List<uint> indices = new List<uint>()
            {
                0, 1, 3,
                1, 2, 3,
            };

            _model = new Model(vertices, indices);

            _meshRenderer.SetModel(_model);

            _shader.Initialize(@"
                    struct vs_in {
                        float3 position_local : POS;
                    };

                    struct vs_out {
                        float4 position_clip : SV_POSITION;
                    };

                    vs_out vs_main(vs_in input) {
                        vs_out output = (vs_out)0;
                        output.position_clip = float4(input.position_local, 1.0);
                        return output;
                    }
                ",
                @"
                    struct vs_out {
                        float4 position_clip : SV_POSITION;
                    };

                    float4 ps_main(vs_out input) : SV_TARGET {
                        return float4( 1.0, 0.5, 0.2, 1.0 );
                    }
                ");
        }

        private void OnWindowClosing()
        {
            _isRunning = false;
        }
    }
}
