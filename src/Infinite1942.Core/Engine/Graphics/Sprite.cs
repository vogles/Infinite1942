using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infinite1942.Engine
{
    public class Sprite
    {
        private Texture2D _texture;
        private Rectangle _sourceRect;
        // private Vector2[] _uvs;

        public Texture2D Texture => _texture;
        public Rectangle SourceRectangle => _sourceRect;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            _sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
        }
        
        public Sprite(Texture2D texture, Rectangle sourceRect)
        {
            _texture = texture;
            _sourceRect = sourceRect;

            // if (texture != null && !_sourceRect.IsEmpty)
            // {
            //     var textureWidth = _texture.Width;
            //     var textureHeight = _texture.Height;
            //
            //     var u = _sourceRect.Left / textureWidth;
            //     var v = _sourceRect.Top / textureHeight;
            //     var normalizedWidth = _sourceRect.Width / textureWidth;
            //     var normalizedHeight = _sourceRect.Height / textureHeight;
            //
            //     var topLeft = new Vector2(u, v);
            //     var topRight = new Vector2(u + normalizedWidth, v);
            //     var bottomLeft = new Vector2(u, v + normalizedHeight);
            //     var bottomRight = new Vector2(u + normalizedWidth, v + normalizedHeight);
            //     
            //     _uvs = new Vector2[]
            //     {
            //         topLeft, 
            //         topRight, 
            //         bottomLeft, 
            //         bottomRight
            //     };
            // }
        }
    }
}