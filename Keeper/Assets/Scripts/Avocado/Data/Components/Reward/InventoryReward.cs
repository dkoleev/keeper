using Avocado.Core;
using Avocado.Core.Factories;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components.Reward {
    [ObjectType("Inventory")]
    public class InventoryReward : IReward {
        public InventoryReward(JObject data) {
            
        }
    }
}