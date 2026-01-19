using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infinite1942.Engine
{
    public class SpriteRenderer : MeshRenderer
    {
        private Texture2D _texture;
        private Sprite _sprite;
        private VertexBuffer _vertexBuffer = null;
        private IndexBuffer _indexBuffer = null;
        private Effect _effect = null;

        public Texture2D Texture
        {
            get => _texture;
            set
            {
                _texture = value;

                if (_effect is BasicEffect basicEffect)
                {
                    basicEffect.TextureEnabled = _texture != null;
                    basicEffect.Texture = _texture;
                }
            }
        }

        public Sprite Sprite
        {
            get => _sprite;
            set
            {
                _sprite = value;

                Texture = _sprite.Texture;
                
                // Regenerate vertex buffer with new UV coordinates
                if (_vertexBuffer != null)
                {
                    _vertexBuffer.Dispose();
                    _vertexBuffer = null;
                }
            }
        }
        
        public void Initialize(GraphicsDevice device)
        {
            _vertexBuffer = GenerateQuadVertexBuffer(device);
            _indexBuffer = GenerateQuadIndexBuffer(device);
            _effect = GenerateBasicEffect(device);
        }

        public override void Draw(GameTime time, GraphicsDevice device, Matrix view, Matrix projection)
        {
            // Regenerate vertex buffer with current sprite UVs if needed
            if (_vertexBuffer == null && device != null)
            {
                _vertexBuffer = GenerateQuadVertexBuffer(device);
            }
            
            // Update the vertex buffer
            device.SetVertexBuffer(_vertexBuffer);
            device.Indices = _indexBuffer;

            if (_effect is BasicEffect basicEffect)
            {
                var transform = gameObject.Transform;
                basicEffect.World = transform.WorldMatrix;
                basicEffect.View = view;
                basicEffect.Projection = projection;
                
                _effect.CurrentTechnique.Passes[0].Apply();
            }
            
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _indexBuffer.IndexCount / 3);
            // device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 3);
            
            base.Draw(time, device, view, projection);
        }

        private IndexBuffer GenerateQuadIndexBuffer(GraphicsDevice device)
        {
            var indices = new ushort[] { 0, 1, 2, 2, 3, 0 };
            
            var buffer = new IndexBuffer(device, typeof(ushort), 6, BufferUsage.WriteOnly);
            buffer.SetData(indices);

            return buffer;
        }
        
        private VertexBuffer GenerateQuadVertexBuffer(GraphicsDevice device)
        {
            var topLeftPos = new Vector3(-0.5f, 0.5f, 0);
            var topRightPos = new Vector3(0.5f, 0.5f, 0);
            var bottomRightPos = new Vector3(0.5f, -0.5f, 0);
            var bottomLeftPos = new Vector3(-0.5f, -0.5f, 0);
            
            // Calculate UV coordinates based on sprite source rectangle
            Vector2 topLeftUV = Vector2.Zero;
            Vector2 topRightUV = Vector2.UnitX;
            Vector2 bottomLeftUV = Vector2.UnitY;
            Vector2 bottomRightUV = Vector2.One;

            if (_sprite != null && _texture != null)
            {
                var sourceRect = _sprite.SourceRectangle;
                var textureWidth = _texture.Width;
                var textureHeight = _texture.Height;
                
                var u = sourceRect.Left / (float)textureWidth;
                var v = sourceRect.Top / (float)textureHeight;
                var normalizedWidth = sourceRect.Width / (float)textureWidth;
                var normalizedHeight = sourceRect.Height / (float)textureHeight;
                
                topLeftUV = new Vector2(u, v);
                topRightUV = new Vector2(u + normalizedWidth, v);
                bottomLeftUV = new Vector2(u, v + normalizedHeight);
                bottomRightUV = new Vector2(u + normalizedWidth, v + normalizedHeight);
            }

            var vertices = new VertexPositionColorNormalTexture[]
            {
                new VertexPositionColorNormalTexture(bottomLeftPos, Color.White, Vector3.Backward, bottomLeftUV),
                new VertexPositionColorNormalTexture(topLeftPos, Color.White, Vector3.Backward, topLeftUV),
                new VertexPositionColorNormalTexture(topRightPos, Color.White, Vector3.Backward, topRightUV),
                new VertexPositionColorNormalTexture(bottomRightPos, Color.White, Vector3.Backward, bottomRightUV),
                // new VertexPositionColorNormalTexture(bottomLeftPos, Color.White, Vector3.Backward, Vector2.UnitY)
            };
            
            var buffer = new VertexBuffer(device, typeof(VertexPositionColorNormalTexture), vertices.Length, BufferUsage.WriteOnly);
            buffer.SetData<VertexPositionColorNormalTexture>(vertices);

            return buffer;
        }

        private Effect GenerateBasicEffect(GraphicsDevice device)
        {
            var basicEffect = new BasicEffect(device);
            basicEffect.World = basicEffect.View = basicEffect.Projection = Matrix.Identity;

            return basicEffect;
        }
    }
}