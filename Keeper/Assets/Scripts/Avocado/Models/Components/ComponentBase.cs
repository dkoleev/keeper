using System;
using Avocado.Data;
using Avocado.Models.Entities;

namespace Avocado.Models.Components {
    [Serializable]
    public abstract class ComponentBase<TComponentData> : IComponent
        where TComponentData : IComponentData {
        public string Type { get; }
        public Entity Entity { get; }
        public TComponentData Data { get; }

        protected ComponentBase(string type, Entity entity, TComponentData data) {
            Type = type;
            Entity = entity;
            Data = data;
        }

        public bool Initialized { get; protected set; }

        public virtual void Initialize() {
            
        }
        
        public virtual void Update() { }
    }
}