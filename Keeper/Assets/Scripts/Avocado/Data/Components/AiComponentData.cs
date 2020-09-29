using System;
using Avocado.Data;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data.Components {
    [Serializable]
    [UsedImplicitly]
    [ComponentType(ComponentType.AI)]
    public readonly struct AiComponentData : IComponentData {
        public readonly string AiType;

        public AiComponentData(JObject data) {
            AiType = data["AiType"].Value<string>();
        }
    }
}