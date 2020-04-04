using Avocado.Framework.Patterns.AbstractFactory;
using Avocado.Game.Entities;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Avocado.Game.Data.Components {
    [UsedImplicitly]
    [ObjectType("Move")]
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