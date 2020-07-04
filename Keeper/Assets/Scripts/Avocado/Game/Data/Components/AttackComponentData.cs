using Avocado.Game.Components;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data.Components {
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