using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Weapon)]
    public class WeaponComponent : ComponentBase<WeaponComponentData> {
        public bool IsAttack { get; set; }
        public string WeaponType => Data.WeaponType;
        public int Damage => Data.Damage;
        public int Range => Data.Range;
        public int Ammo {
            get => _currentAmmo;
            set => _currentAmmo = value;
        }
        public string Prefab => Data.Prefab;

        private int _currentAmmo;

        public WeaponComponent(Entity entity, WeaponComponentData data) : base(entity, data) {
            _currentAmmo = Data.Ammo;
        }
    }
}