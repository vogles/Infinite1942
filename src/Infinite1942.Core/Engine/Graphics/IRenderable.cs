using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infinite1942.Engine
{
    public interface IRenderable : IComponent
    {
        void Draw(GameTime time, GraphicsDevice device, Matrix view, Matrix projection);
    }
}