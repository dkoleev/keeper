using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Move)]
    public readonly struct MoveComponent : IComponent {
        public Entity Entity { get; }

        private readonly MoveComponentData _data;
        
        public MoveComponent(Entity entity, IComponentData data)
        {
            Entity = entity;
            _data = (MoveComponentData) data;
        }
    }
}