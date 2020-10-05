using System.Collections.Generic;
using Avocado.Core;
using Avocado.Core.Factories;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components.Reward {
    [ObjectType("Simple")]
    public class SimpleReward : IReward {
        public readonly IReadOnlyDictionary<string, int> Content;
        public SimpleReward(JObject data) {
            Content = data["Content"].ToObject<IReadOnlyDictionary<string, int>>();
        }
    }
}