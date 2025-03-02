using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite1942.Graphics
{
    public class DXMeshRenderer : MeshRenderer
    {
        private DXGraphicsDevice _device;

        public void Initialize(DXGraphicsDevice device)
        {
            _device = device;
        }

        public override void SetModel(Model model)
        {
            _model = model;
        }

        public override void Render()
        {
        }
    }
}
