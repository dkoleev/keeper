using System.Collections.Generic;
using Avocado.Core.Factories.Components;
using Avocado.Game.Data;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ComponentType(ComponentType.Inventory)]
    public class InventoryData : BaseComponentData {
        public readonly int Size;
        public readonly IReadOnlyDictionary<string, int> Content;

        public InventoryData(JObject data) : base(data) { }
    }
}