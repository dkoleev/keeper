using Avocado.Core.Factories;
using Avocado.Core.Factories.Components;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ObjectType(ComponentTypes.AI)]
    public class AiComponentData : BaseComponentData {
        public readonly string AiType;
        public AiComponentData(JObject data) : base(data) { }
    }
}