using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Health)]
    public class HealthComponent : ComponentBase<HealthComponentData> {
        public int CurrentHealth { get; set; }

        public HealthComponent(Entity entity, HealthComponentData data) : base(entity, data)
        {
            CurrentHealth = Data.MaxHealth;
        }
    }
}