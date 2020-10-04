using Avocado.Game.Data;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ComponentType(ComponentType.Move)]
    public class MoveComponentData : BaseComponentData
    {
        public readonly byte SpeedMove;
        public readonly byte SpeedRotate;

        public MoveComponentData(JObject data) : base(data) {
            var s = SpeedMove;
        }
    }
}