using Microsoft.Xna.Framework;

namespace Infinite1942.Engine
{
    public class Camera : IComponent
    {
        public Matrix View { get; set; }
        public Matrix Projection { get; set; }
        public GameObject gameObject { get; set; }
    }
}
