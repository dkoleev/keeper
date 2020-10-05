using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Data.Components;
using Avocado.Models.Entities;
using JetBrains.Annotations;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Weapon)]
    public class WeaponComponent : ComponentBase<WeaponComponentData> {
        public bool IsAttack { get; set; }
        public int Damage => Data.Damage;
        public float Delay => Data.Delay;
        public float CurrentDelay { get; set; }
        public float Range => Data.Range;
        public int Ammo {
            get => _currentAmmo;
            set => _currentAmmo = value;
        }

        private int _currentAmmo;

        public WeaponComponent(string type, Entity entity, WeaponComponentData data) : base(type, entity, data) {
            _currentAmmo = Data.Clip;
        }
    }
}