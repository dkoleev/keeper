using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ObjectType(ComponentTypes.PlayerControls)]
    public class PlayerControlsComponentData : BaseComponentData {
        public PlayerControlsComponentData(JObject data) : base(data) { }
    }
}