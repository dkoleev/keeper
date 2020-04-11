using Avocado.Game.Components;
using JetBrains.Annotations;

namespace Avocado.Game.Data.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Weapon)]
    public struct WeaponComponentData : IComponentData {
        public readonly float Damage;
        public readonly float Delay;
        public readonly int Clip;
        public readonly float Range;

        public WeaponComponentData(float damage, float delay, int clip, float range) {
            Damage = damage;
            Delay = delay;
            Clip = clip;
            Range = range;
        }
    }
}