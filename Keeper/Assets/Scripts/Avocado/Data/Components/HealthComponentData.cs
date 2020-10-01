using Avocado.Game.Data;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components
{
    [UsedImplicitly]
    [ComponentType(ComponentType.Health)]
    public readonly struct HealthComponentData : IComponentData
    {
        public readonly int MaxHealth;

        public HealthComponentData(JObject data)
        {
            MaxHealth = data["MaxHealth"].Value<int>();
        }
    }
}