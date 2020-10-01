using Avocado.Game.Data;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Weapon)]
    public struct WeaponComponentData : IComponentData {
        public readonly int Damage;
        public readonly float Delay;
        public readonly int Clip;
        public readonly float Range;

        public WeaponComponentData(JObject data) {
            Damage = data["Damage"].Value<int>();
            Delay = data["Delay"].Value<float>();
            Clip = data["Clip"].Value<int>();
            Range = data["Range"].Value<float>();
        }
    }
}