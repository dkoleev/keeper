using System;
using Avocado.Core.Factories.Components;
using Avocado.Game.Data;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [Serializable]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ComponentType(ComponentType.AI)]
    public class AiComponentData : BaseComponentData {
        public readonly string AiType;
        public AiComponentData(JObject data) : base(data) { }
    }
}