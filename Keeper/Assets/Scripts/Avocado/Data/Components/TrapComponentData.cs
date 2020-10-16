using Avocado.Core.Factories.ObjectTypes;
using Avocado.Data.Components.TrapVariants;
using Avocado.Framework.Patterns.Factory;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Trap)]
    public class TrapComponentData : BaseComponentData {
        public readonly string TrapType;
        
        [JsonIgnore]
        public readonly ITrapData Trap;
        
        public TrapComponentData(JObject data) : base(data) {
            var factory = new Factory<ITrapData>();
            Trap = factory.Create(TrapType, data);
        }
    }
}