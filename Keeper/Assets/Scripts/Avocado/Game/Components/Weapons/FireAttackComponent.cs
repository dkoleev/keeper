using Avocado.Game.Data;
using Avocado.Game.Data.Components.Weapons;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components.Weapons {
    [UsedImplicitly]
    [ComponentType(ComponentType.FireAttack)]
    public class FireAttackComponent : ComponentBase<FireAttackComponentData> {
        public bool IsAttack { get; set; }
        public int Damage => Data.Damage;
        public int Range => Data.Range;
        public int Ammo {
            get => _currentAmmo;
            set => _currentAmmo = value;
        }

        private int _currentAmmo;

        public FireAttackComponent(Entity entity, FireAttackComponentData data) : base(entity, data) {
            _currentAmmo = Data.Ammo;
        }
    }
}
