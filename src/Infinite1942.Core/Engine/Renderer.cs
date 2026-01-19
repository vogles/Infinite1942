using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infinite1942.Engine
{
    public class Renderer : IRenderable
    {
        public GameObject gameObject { get; set; }
        
        public virtual void Draw(GameTime time, GraphicsDevice device, Matrix view, Matrix projection) { }
    }
}
