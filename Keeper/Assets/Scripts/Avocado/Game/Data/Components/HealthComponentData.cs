using Avocado.Game.Components;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data.Components
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