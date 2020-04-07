using Avocado.Game.Components;
using JetBrains.Annotations;

namespace Avocado.Game.Data.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Move)]
    public readonly struct MoveComponentData : IComponentData
    {
        public readonly byte SpeedMove;
        public readonly byte SpeedRotate;

        public MoveComponentData(byte speedMove, byte speedRotate)
        {
            SpeedMove = speedMove;
            SpeedRotate = speedRotate;
        }
    }
}