using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components.Reward {
    [UsedImplicitly]
    [ObjectType(RewardTypes.Inventory)]
    public class InventoryReward : IRewardData {
        public InventoryReward(JObject data) {
            
        }
    }
}