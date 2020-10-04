using System.Collections.Generic;

namespace Avocado.Data.Components.Reward {
    public class CompositeReward : IReward {
        private IList<IReward> _rewards;

        public CompositeReward() {
            _rewards = new List<IReward>();
        }
    }
}