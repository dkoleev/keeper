using Avocado.Framework.Patterns.AbstractFactory;
using JetBrains.Annotations;

namespace Avocado.Game.Data.Components {
    [UsedImplicitly]
    [ObjectType("Weapon")]
    public readonly struct WeaponComponentData : IComponentData {
        public readonly string WeaponType;
        public readonly int Damage;
        public readonly int Ammo;
        public readonly int Range;
        public readonly string Prefab;

        public WeaponComponentData(string weaponType, int damage, int ammo, int range, string prefab) {
            WeaponType = weaponType;
            Damage = damage;
            Ammo = ammo;
            Range = range;
            Prefab = prefab;
        }
    }
}