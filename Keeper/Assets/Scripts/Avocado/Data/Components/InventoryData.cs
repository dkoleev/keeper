using System.Collections.Generic;
using Avocado.Framework.Patterns.Factory;
using Avocado.Core.Factories.ObjectTypes;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ObjectType(ComponentTypes.Inventory)]
    public class InventoryData : BaseComponentData {
        public readonly int Size;
        public readonly IReadOnlyDictionary<string, int> Content;

        public InventoryData(JObject data) : base(data) { }
    }
}