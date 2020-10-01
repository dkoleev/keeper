using Avocado.Game.Data;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Attack)]
    public readonly struct AttackComponentData : IComponentData {
        public readonly string Weapon;
        public readonly int StartAmmo;

        public AttackComponentData(JObject data) {
            Weapon = data["Weapon"].Value<string>();
            StartAmmo = data["StartAmmo"].Value<int>();
        }
    }
}