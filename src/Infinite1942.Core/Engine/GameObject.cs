using System.Collections.Generic;

namespace Infinite1942.Engine
{
    public class GameObject
    {
        private List<IComponent> _components = new List<IComponent>();

        public Transform Transform { get; private set; }

        public GameObject()
        {
            Transform = AddComponent<Transform>();
        }

        public T AddComponent<T>() where T : IComponent, new()
        {
            var newComponent = new T();
            newComponent.gameObject = this;
            _components.Add(newComponent);
            return newComponent;
        }
    }
}
