using System;
using System.Drawing;
using Silk.NET.Core.Contexts;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Infinite1942
{
    public class Game
    {
        private IWindow _window = null;
        private GL _gl = null;
        private Vector2D<int> _windowSize;
        private string _windowTitle;
        private bool _isRunning;

        // Temp variables to test functionality
        private uint _vao;
        private uint _vbo;
        private uint _ebo;
        private uint _shaderProgram;

        public Game() : this("Game", 800, 600) { }

        public Game(string title, int width, int height)
        {
            _windowTitle = title;
            _windowSize = new Vector2D<int>(width, height);
        }

        public void Run()
        {
            Initialize();

            _isRunning = true;
            while (_isRunning)
            {
                _window?.DoEvents();

                Render();
            }

            Cleanup();
        }

        private unsafe void Initialize()
        {
            _window = Platform.CreateWindow(_windowTitle, _windowSize);

            if (_window != null)
            {
                _window.Closing += OnWindowClosing;
                _window.Initialize();
            }

            _gl = GL.GetApi(_window);
            // _gl = _window.CreateOpenGL();

            if (_gl != null)
            {
                _gl.ClearColor(Color.CornflowerBlue);

                _vao = _gl.GenVertexArray();
                _gl.BindVertexArray(_vao);

                float[] vertices =
                {
                    0.5f,  0.5f, 0.0f,
                    0.5f, -0.5f, 0.0f,
                    -0.5f, -0.5f, 0.0f,
                    -0.5f,  0.5f, 0.0f
                };

                uint[] indices =
                {
                    0u, 1u, 3u,
                    1u, 2u, 3u
                };

                _vbo = _gl.GenBuffer();
                _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

                fixed (float* v = vertices)
                {
                    _gl.BufferData(
                        BufferTargetARB.ArrayBuffer,
                        (nuint)(vertices.Length * sizeof(float)),
                        v,
                        BufferUsageARB.StaticDraw);
                }

                _ebo = _gl.GenBuffer();
                _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);

                fixed (uint* i = indices)
                {
                    _gl.BufferData(
                        BufferTargetARB.ElementArrayBuffer,
                        (nuint)(indices.Length * sizeof(uint)),
                        i,
                        BufferUsageARB.StaticDraw);
                }

                string vertexShaderCode = @"#version 330 core

                    layout (location = 0) in vec3 aPosition;

                    void main()
                    {
                        gl_Position = vec4(aPosition, 1.0);
                    }
                ";

                string fragmentShaderCode = @"#version 330 core

                    out vec4 FragColor;

                    void main()
                    {
                        FragColor = vec4(1.0, 0.5, 0.2, 1.0);
                    }
                ";

                uint vertexShader = _gl.CreateShader(ShaderType.VertexShader);
                _gl.ShaderSource(vertexShader, vertexShaderCode);
                _gl.CompileShader(vertexShader);

                _gl.GetShader(vertexShader, ShaderParameterName.CompileStatus, out int vStatus);
                if (vStatus != (int)GLEnum.True)
                {
                    throw new Exception("Vertex shader failed to compile: " + _gl.GetShaderInfoLog(vertexShader));
                }

                uint fragmentShader = _gl.CreateShader(ShaderType.FragmentShader);
                _gl.ShaderSource(fragmentShader, fragmentShaderCode);
                _gl.CompileShader(fragmentShader);

                _gl.GetShader(fragmentShader, ShaderParameterName.CompileStatus, out int fStatus);
                if (fStatus != (int)GLEnum.True)
                {
                    throw new Exception("Fragment shader failed to compile: " + _gl.GetShaderInfoLog(fragmentShader));
                }

                _shaderProgram = _gl.CreateProgram();
                _gl.AttachShader(_shaderProgram, vertexShader);
                _gl.AttachShader(_shaderProgram, fragmentShader);
                _gl.LinkProgram(_shaderProgram);

                _gl.GetProgram(_shaderProgram, ProgramPropertyARB.LinkStatus, out int pStatus);
                if (pStatus != (int)GLEnum.True)
                {
                    throw new Exception("Shader program failed to link: " + _gl.GetProgramInfoLog(_shaderProgram));
                }

                _gl.DetachShader(_shaderProgram, vertexShader);
                _gl.DetachShader(_shaderProgram, fragmentShader);
                _gl.DeleteShader(vertexShader);
                _gl.DeleteShader(fragmentShader);

                _gl.EnableVertexAttribArray(0);
                _gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), null);

                _gl.BindVertexArray(0);
                _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
                _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
            }
        }

        private unsafe void Render()
        {
            _gl.Clear(ClearBufferMask.ColorBufferBit);

            _gl.BindVertexArray(_vao);
            _gl.UseProgram(_shaderProgram);
            _gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, null);

            ((IGLContext)_gl.Context).SwapBuffers();
        }

        private void Cleanup()
        {
            _gl?.DeleteBuffer(_vbo);
            _gl?.DeleteBuffer(_ebo);
            _gl?.DeleteVertexArray(_vao);
            _gl?.DeleteProgram(_shaderProgram);

            _window?.Dispose();
        }

        private void OnWindowClosing()
        {
            _isRunning = false;
        }
    }
}
