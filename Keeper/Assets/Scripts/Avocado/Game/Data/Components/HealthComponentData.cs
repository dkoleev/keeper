using Avocado.Game.Components;
using JetBrains.Annotations;

namespace Avocado.Game.Data.Components
{
    [UsedImplicitly]
    [ComponentType(ComponentType.Health)]
    public readonly struct HealthComponentData : IComponentData
    {
        public readonly int MaxHealth;

        public HealthComponentData(int maxHealth)
        {
            MaxHealth = maxHealth;
        }
    }
}