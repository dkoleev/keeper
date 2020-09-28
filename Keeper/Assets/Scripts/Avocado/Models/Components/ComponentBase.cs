using System;
using Avocado.Game.Data;
using Avocado.Models.Entities;

namespace Avocado.Models.Components {
    [Serializable]
    public abstract class ComponentBase<TComponentData> : IComponent
        where TComponentData : IComponentData {
        public Entity Entity { get; }
        
        protected TComponentData Data { get; }

        protected ComponentBase(Entity entity, TComponentData data) {
            Entity = entity;
            Data = data;
        }

        public bool Initialized { get; protected set; }

        public virtual void Initialize() {
            
        }
        
        public virtual void Update() { }
    }
}