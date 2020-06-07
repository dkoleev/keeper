using System;
using Avocado.Game.Data;
using Avocado.Game.Entities;

namespace Avocado.Game.Components {
    [Serializable]
    public abstract class ComponentBase<TComponentData> : IComponent
        where TComponentData : IComponentData {
        public Entity Entity { get; }
        public virtual void Update() { }

        protected TComponentData Data { get; }

        protected ComponentBase(Entity entity, TComponentData data) {
            Entity = entity;
            Data = data;
        }

        public bool Initialized { get; protected set; }

        public virtual void Initialize() {
            
        }
    }
}