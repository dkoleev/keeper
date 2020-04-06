using Avocado.Framework.Patterns.AbstractFactory;
using JetBrains.Annotations;

namespace Avocado.Game.Data.Components.Weapons {
    [UsedImplicitly]
    [ObjectType("Weapon")]
    public struct WeaponComponentData : IComponentData {
        public readonly int Damage;
        public readonly int Clip;
        public readonly int Range;

        public WeaponComponentData(int damage, int clip, int range) {
            Damage = damage;
            Clip = clip;
            Range = range;
        }
    }
}