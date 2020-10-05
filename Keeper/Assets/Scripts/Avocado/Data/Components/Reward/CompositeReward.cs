using System.Collections.Generic;
using Avocado.Core;
using Avocado.Core.Factories;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components.Reward {
    [ObjectType("Composite")]
    public class CompositeReward : IReward {
        private IList<IReward> _rewards;

        public CompositeReward(JObject data) {
            _rewards = new List<IReward>();
        }
    }
}