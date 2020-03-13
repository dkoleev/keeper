using Avocado.Framework.Patterns.AbstractFactory;
using JetBrains.Annotations;

namespace Avocado.Game.Data.Components {
    [UsedImplicitly]
    [ObjectType("Move")]
    public readonly struct MoveComponentData : IComponentData {
        public readonly float Speed;

        public MoveComponentData(float speed) {
            Speed = speed;
        }
    }
}