using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Health)]
    public readonly struct HealthComponent : IComponent {
        public Entity Entity { get; }

        private readonly HealthComponentData _data;
        
        public HealthComponent(Entity entity, IComponentData data)
        {
            Entity = entity;
            _data = (HealthComponentData) data;
        }
    }
}