using System.Collections.Generic;
using Avocado.Data;
using Avocado.Data.Components;
using Avocado.Game.Data;
using Avocado.Models.Entities;
using JetBrains.Annotations;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Inventory)]
    public class Inventory : ComponentBase<InventoryData> {
        private IDictionary<string, int> _content;
        public Inventory(Entity entity, InventoryData data) : base(entity, data) {
            _content = new Dictionary<string, int>();
            foreach (var resource in data.Content) {
                _content[resource.Key] = resource.Value;
            }
        }
    }
}