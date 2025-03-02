using Silk.NET.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite1942
{
    public class Model
    {
        private List<Vector3D<float>> _vertices = new List<Vector3D<float>>();
        private List<uint> _indices = new List<uint>();

        public List<Vector3D<float>> Vertices { get { return _vertices; } }
        public List<uint> Indices { get { return _indices; } }

        public Model() { }
        public Model(List<Vector3D<float>> vertices, List<uint> indices)
        {
            _vertices.AddRange(vertices);
            _indices.AddRange(indices);
        }

        public void SetVertices(List<Vector3D<float>> vertices)
        {
            _vertices.Clear();
            _vertices.AddRange(vertices);
        }
    }
}
