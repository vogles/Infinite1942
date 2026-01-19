namespace Infinite1942.Engine
{
    public class MeshRenderer : Renderer
    {
        private Mesh _mesh = null;
        
        public  Mesh Mesh
        {
            get => _mesh;
            protected  set => _mesh = value;
        }
    }
}