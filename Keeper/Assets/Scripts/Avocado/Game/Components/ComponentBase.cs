using Avocado.Game.Data;
using Avocado.Game.Entities;

namespace Avocado.Game.Components {
    public abstract class ComponentBase<TComponentData> : IComponent
        where TComponentData : IComponentData {
        public Entity Entity { get; }
        protected TComponentData Data { get; }

        public ComponentBase(Entity entity, TComponentData data) {
            Entity = entity;
            Data = data;
        }
    }
}
