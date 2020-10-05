using Avocado.Core.Factories.Components;
using Avocado.Data;
using Avocado.Data.Components;
using Avocado.Game.Data;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using Sigtrap.Relays;
using UnityEngine;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Health)]
    public class HealthComponent : ComponentBase<HealthComponentData> {
        public readonly Relay<HealthComponent> OnDead = new Relay<HealthComponent>();
        public int CurrentHealth { get; private set; }
        public bool IsAlive => CurrentHealth > 0;

        public HealthComponent(Entity entity, HealthComponentData data) : base(entity, data) {
            CurrentHealth = Data.MaxHealth;
        }

        public override void Initialize() {
            base.Initialize();

            Initialized = true;
        }

        public void Damage(int value) {
            CurrentHealth = Mathf.Max(0, CurrentHealth - value);
            if (CurrentHealth == 0) {
                OnDead.Dispatch(this);
            }
        }

        public void Heal(int value) {
            CurrentHealth += value;
        }
    }
}