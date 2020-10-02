using Avocado.Data;
using Avocado.Data.Components;
using Avocado.Game.Data;
using Avocado.Models.Entities;
using JetBrains.Annotations;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Inventory)]
    public class Inventory : ComponentBase<InventoryData> {
        public Inventory(Entity entity, InventoryData data) : base(entity, data) {
            
        }
    }
}