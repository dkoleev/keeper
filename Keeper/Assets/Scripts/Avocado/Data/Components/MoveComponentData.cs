using Avocado.Core.Factories;
using Avocado.Core.Factories.Components;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ObjectType(ComponentTypes.Move)]
    public class MoveComponentData : BaseComponentData
    {
        public readonly byte SpeedMove;
        public readonly byte SpeedRotate;

        public MoveComponentData(JObject data) : base(data) { }
    }
}