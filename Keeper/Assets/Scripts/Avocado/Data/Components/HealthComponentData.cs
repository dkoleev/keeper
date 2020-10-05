using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ObjectType(ComponentTypes.Health)]
    public class HealthComponentData : BaseComponentData
    {
        public readonly int MaxHealth;
        public readonly int StartHealth;
        public HealthComponentData(JObject data) : base(data) { }
    }
}