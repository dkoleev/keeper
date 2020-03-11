using System;
using Avocado.Game.Entities;

namespace Avocado.Game.Components
{
    [Serializable]
    public class ComponentBase {
        protected Entity Entity;
        protected bool Initialized;

        public virtual void Initialize(Entity entity) {
            Entity = entity;
        }

        public virtual void Update() {
            
        }
    }
}