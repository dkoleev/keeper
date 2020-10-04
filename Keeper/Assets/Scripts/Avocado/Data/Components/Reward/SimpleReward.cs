using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components.Reward {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class SimpleReward : IReward {
        public readonly IReadOnlyDictionary<string, int> Content;
        public SimpleReward(JObject data) {
            Content = data["Content"].ToObject<IReadOnlyDictionary<string, int>>();
        }
    }
}