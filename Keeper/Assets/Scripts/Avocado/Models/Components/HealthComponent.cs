using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Health)]
    public class HealthComponent : ComponentBase<HealthComponentData> {
        public int CurrentHealth { get; private set; }
        public bool IsAlive => CurrentHealth > 0;

        public HealthComponent(Entity entity, HealthComponentData data) : base(entity, data) {
            CurrentHealth = Data.MaxHealth;
        }

        public void Damage(int value) {
            CurrentHealth = Mathf.Max(0, CurrentHealth - value);
            if (CurrentHealth == 0) {
                
            }
        }

        public void Heal(int value) {
            CurrentHealth += value;
        }
    }
}