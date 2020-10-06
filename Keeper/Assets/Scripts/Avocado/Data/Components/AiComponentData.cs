using Avocado.Core.Factories.ObjectTypes;
using Avocado.Framework.Patterns.Factory;
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