using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Health)]
    public struct HealthComponent : IComponent {
        public Entity Entity { get; private set; }

        private HealthComponentData _data;

        public void Initialize(Entity entity, IComponentData data) {
            Entity = entity;
            _data = (HealthComponentData) data;
        }

        public void Update() {
            
        }
    }
}