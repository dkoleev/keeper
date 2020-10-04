using Avocado.Game.Data;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ComponentType(ComponentType.Health)]
    public class HealthComponentData : BaseComponentData
    {
        public readonly int MaxHealth;
        public readonly int StartHealth;
        public HealthComponentData(JObject data) : base(data) { }
    }
}