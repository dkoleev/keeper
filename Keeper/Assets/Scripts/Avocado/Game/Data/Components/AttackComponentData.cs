using Avocado.Framework.Patterns.AbstractFactory;
using JetBrains.Annotations;

namespace Avocado.Game.Data.Components {
    [UsedImplicitly]
    [ObjectType("Attack")]
    public readonly struct AttackComponentData : IComponentData {
        public readonly string CurrentWeapon;

        public AttackComponentData(string currentWeapon) {
            CurrentWeapon = currentWeapon;
        }
    }
}