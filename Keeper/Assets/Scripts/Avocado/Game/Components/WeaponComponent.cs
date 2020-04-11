using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Weapon)]
    public class WeaponComponent : ComponentBase<WeaponComponentData> {
        public bool IsAttack { get; set; }
        public float Damage => Data.Damage;
        public float Delay => Data.Delay;
        public float CurrentDelay { get; set; }
        public float Range => Data.Range;
        public int Ammo {
            get => _currentAmmo;
            set => _currentAmmo = value;
        }

        private int _currentAmmo;

        public WeaponComponent(Entity entity, WeaponComponentData data) : base(entity, data) {
            _currentAmmo = Data.Clip;
        }
    }
}
