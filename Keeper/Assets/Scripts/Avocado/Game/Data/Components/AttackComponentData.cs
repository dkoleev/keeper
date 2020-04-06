using Avocado.Framework.Patterns.AbstractFactory;
using JetBrains.Annotations;

namespace Avocado.Game.Data.Components {
    [UsedImplicitly]
    [ObjectType("Attack")]
    public readonly struct AttackComponentData : IComponentData {
        public readonly string Weapon;
        public readonly int StartAmmo;

        public AttackComponentData(string currentWeapon, int startAmmo) {
            Weapon = currentWeapon;
            StartAmmo = startAmmo;
        }
    }
}