using Avocado.Game.Components;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Move)]
    public readonly struct MoveComponentData : IComponentData
    {
        public readonly byte SpeedMove;
        public readonly byte SpeedRotate;

        public MoveComponentData(JObject data)
        {
            SpeedMove = data["SpeedMove"].Value<byte>();
            SpeedRotate = data["SpeedRotate"].Value<byte>();
        }
    }
}