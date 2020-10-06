using System.Collections.Generic;
using Avocado.Framework.Patterns.Factory;
using Avocado.Core.Factories.ObjectTypes;
using JetBrains.Annotations;

namespace Avocado.Models.Components.Rewards {
    [UsedImplicitly]
    [ObjectType(RewardTypes.Inventory)]
    public class InventoryReward : IReward {
        public IReadOnlyDictionary<string, int> Content { get; }

        public InventoryReward(RewardComponent reward) {
            var inventory = reward.Entity.GetComponentByType<Inventory>() as Inventory;
            if (inventory is null) {
                return;
            }
            
            Content = inventory.Content as IReadOnlyDictionary<string,int>;
        }
    }
}