using Avocado.Framework.Patterns.AbstractFactory;
using JetBrains.Annotations;

namespace Avocado.Game.Data.Components.Weapons {
    [UsedImplicitly]
    [ObjectType("FireAttack")]
    public struct FireAttackComponentData : IComponentData {
        public readonly int Damage;
        public readonly int Ammo;
        public readonly int Range;

        public FireAttackComponentData(int damage, int ammo, int range) {
            Damage = damage;
            Ammo = ammo;
            Range = range;
        }
    }
}