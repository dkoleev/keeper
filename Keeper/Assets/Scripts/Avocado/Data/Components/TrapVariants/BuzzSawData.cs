using Avocado.Core.Factories.ObjectTypes;
using Avocado.Framework.Patterns.Factory;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components.TrapVariants {
    [UsedImplicitly]
    [ObjectType(TrapTypes.BuzzSaw)]
    public class BuzzSawData : ITrapData {
        public readonly int Damage;
        public readonly int Size;
        
        public BuzzSawData(JObject data) {
            Damage = data["Damage"].Value<int>();
            Size = data["Size"].Value<int>();
        }
    }
}