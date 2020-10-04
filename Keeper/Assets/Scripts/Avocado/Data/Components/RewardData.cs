using System.Collections.Generic;
using Avocado.Game.Data;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ComponentType(ComponentType.Reward)]
    public class RewardData : BaseComponentData {
        public readonly string Trigger;
        public readonly IReadOnlyDictionary<string, int> Content;
        
        public RewardData(JObject data) : base(data) { }
    }
}
