using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Move)]
    public readonly struct MoveComponent : IComponent {
        public Entity Entity { get; }
        public float SpeedMove => _data.Speed;

        private readonly MoveComponentData _data;
        
        public MoveComponent(Entity entity, IComponentData data)
        {
            Entity = entity;
            _data = (MoveComponentData) data;
        }
    }
}