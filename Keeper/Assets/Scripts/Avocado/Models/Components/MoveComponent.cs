using Avocado.Core.Factories;
using Avocado.Core.Factories.Components;
using Avocado.Data.Components;
using Avocado.Models.Entities;
using JetBrains.Annotations;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Move)]
    public class MoveComponent : ComponentBase<MoveComponentData> {
        public float SpeedMove => Data.SpeedMove;
        public float CurrentSpeedMove { get; set; }
        public float SpeedRotate => Data.SpeedRotate;
        public bool IsMoving => CurrentSpeedMove > 0.01f;

        public MoveComponent(string type, Entity entity, MoveComponentData data) : base(type, entity, data)
        {
            CurrentSpeedMove = 0;
        }
    }
}