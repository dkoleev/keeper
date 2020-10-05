using Avocado.Core.Factories;
using Avocado.Core.Factories.Components;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ObjectType(ComponentTypes.Attack)]
    public class AttackComponentData : BaseComponentData {
        public readonly string Weapon;
        public readonly int StartAmmo;

        public AttackComponentData(JObject data) : base(data) { }
    }
}