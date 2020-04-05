using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Weapon)]
    public class WeaponComponent : IComponent {
        public Entity Entity { get; }
        public bool IsAttack { get; set; }

        public string WeaponType => _data.WeaponType;
        public int Damage => _data.Damage;
        public int Range => _data.Range;
        public int Ammo {
            get => _currentAmmo;
            set => _currentAmmo = value;
        }
        public string Prefab => _data.Prefab;

        private WeaponComponentData _data;
        private int _currentAmmo;

        public WeaponComponent(Entity entity, IComponentData data) {
            Entity = entity;
            _data = (WeaponComponentData) data;
            _currentAmmo = _data.Ammo;
        }
    }
}
