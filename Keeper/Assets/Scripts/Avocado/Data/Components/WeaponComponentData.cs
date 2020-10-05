using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ObjectType(ComponentTypes.Weapon)]
    public class WeaponComponentData : BaseComponentData {
        public readonly int Damage;
        public readonly float Delay;
        public readonly int Clip;
        public readonly float Range;
        public WeaponComponentData(JObject data) : base(data) { }
    }
}