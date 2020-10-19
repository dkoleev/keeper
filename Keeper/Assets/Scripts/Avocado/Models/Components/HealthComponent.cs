using Avocado.Framework.Patterns.Factory;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Data.Components;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using Sigtrap.Relays;
using UnityEngine;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Health)]
    public class HealthComponent : ComponentBase<HealthComponentData> {
        public readonly Relay<HealthComponent> OnDead = new Relay<HealthComponent>();
        public readonly Relay<int, int, int> OnHealthChange = new Relay<int, int, int>();
        public int CurrentHealth { get; private set; }
        public int MaxHealth => Data.MaxHealth;
        public bool IsAlive => CurrentHealth > 0;

        public HealthComponent(string type, Entity entity, HealthComponentData data) : base(type, entity, data) {
            CurrentHealth = Data.MaxHealth;
        }

        public override void Initialize() {
            base.Initialize();

            Initialized = true;
        }

        public void Damage(int value) {
            CurrentHealth = Mathf.Max(0, CurrentHealth - value);
            OnHealthChange.Dispatch(CurrentHealth + value, CurrentHealth, Data.MaxHealth);

            if (CurrentHealth == 0) {
                OnDead.Dispatch(this);
            }
        }

        public void Heal(int value) {
            CurrentHealth += value;
        }
    }
}