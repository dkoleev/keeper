using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Move)]
    public struct MoveComponent : IComponent {
        public Entity Entity { get; }
        public float SpeedMove => _data.SpeedMove;
        public float SpeedRotate => _data.SpeedRotate;

        private readonly MoveComponentData _data;
        
        public MoveComponent(Entity entity, IComponentData data)
        {
            Entity = entity;
            _data = (MoveComponentData) data;
        }
    }
}