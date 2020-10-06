using System.Collections.Generic;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Framework.Patterns.Factory;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components.Reward {
    [UsedImplicitly]
    [ObjectType(RewardTypes.Simple)]
    public class SimpleReward : IRewardData {
        public readonly IReadOnlyDictionary<string, int> Content;
        public SimpleReward(JObject data) {
            Content = data["Content"].ToObject<IReadOnlyDictionary<string, int>>();
        }
    }
}