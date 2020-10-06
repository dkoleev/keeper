using System.Collections.Generic;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Framework.Patterns.Factory;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components.Reward {
    [UsedImplicitly]
    [ObjectType(RewardTypes.Composite)]
    public class CompositeReward : IRewardData {
        private readonly IList<IRewardData> _rewards;

        public CompositeReward(JObject data) {
            _rewards = new List<IRewardData>();
        }
    }
}