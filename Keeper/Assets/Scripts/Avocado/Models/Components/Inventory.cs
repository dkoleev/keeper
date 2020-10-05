using System.Collections.Generic;
using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Data.Components;
using Avocado.Models.Entities;
using JetBrains.Annotations;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Inventory)]
    public class Inventory : ComponentBase<InventoryData> {
        private IDictionary<string, int> _content;
        public Inventory(string type, Entity entity, InventoryData data) : base(type, entity, data) {
            _content = new Dictionary<string, int>();
            foreach (var resource in data.Content) {
                _content[resource.Key] = resource.Value;
            }
        }
    }
}