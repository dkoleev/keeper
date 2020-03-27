using Avocado.Framework.Patterns.AbstractFactory;
using Avocado.Game.Entities;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Avocado.Game.Data.Components
{
    [UsedImplicitly]
    [ObjectType("Health")]
    public readonly struct HealthComponentData : IComponentData
    {
        public readonly int MaxHealth;

        public HealthComponentData(int maxHealth)
        {
            MaxHealth = maxHealth;
        }
    }
}