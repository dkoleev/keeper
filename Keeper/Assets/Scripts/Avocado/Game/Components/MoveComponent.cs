using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Move)]
    public struct MoveComponent : IComponent {
        public Entity Entity { get; private set; }

        private MoveComponentData _data;
        
        public void Initialize(Entity entity, IComponentData data) {
            Entity = entity;
            _data = (MoveComponentData) data;
        }

        public void Update() {
            
        }
    }
}