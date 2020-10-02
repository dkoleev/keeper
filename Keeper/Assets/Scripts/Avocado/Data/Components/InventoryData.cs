using System.Collections.Generic;
using Avocado.Game.Data;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Inventory)]
    public readonly struct InventoryData : IComponentData {
        public readonly int Size;
        public readonly Dictionary<string, int> Content;

        public InventoryData(JObject data) {
            if (data["Size"] is null) {
                throw new KeyNotFoundException("not found key Size in inventory component");
            }

            Size = data["Size"].Value<int>();
            Content = new Dictionary<string, int>();
            if (!(data["Content"] is null)) {
                Content = data["Content"].ToObject<Dictionary<string, int>>();
            }
        }
    }
}