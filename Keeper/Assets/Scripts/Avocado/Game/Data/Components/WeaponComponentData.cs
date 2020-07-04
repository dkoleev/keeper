using Avocado.Game.Components;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Weapon)]
    public struct WeaponComponentData : IComponentData {
        public readonly float Damage;
        public readonly float Delay;
        public readonly int Clip;
        public readonly float Range;

        public WeaponComponentData(JObject data) {
            Damage = data["Damage"].Value<float>();
            Delay = data["Delay"].Value<float>();
            Clip = data["Clip"].Value<int>();
            Range = data["Range"].Value<float>();
        }
    }
}