using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite1942.Graphics
{
    public abstract class MeshRenderer
    {
        protected Model _model;

        public abstract void SetModel(Model model);
        public abstract void Render();
    }
}
