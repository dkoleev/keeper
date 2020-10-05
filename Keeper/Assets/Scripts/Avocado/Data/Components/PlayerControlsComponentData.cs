using Avocado.Core.Factories.Components;
using Avocado.Game.Data;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ComponentType(ComponentType.PlayerControls)]
    public class PlayerControlsComponentData : BaseComponentData {
        public PlayerControlsComponentData(JObject data) : base(data) { }
    }
}