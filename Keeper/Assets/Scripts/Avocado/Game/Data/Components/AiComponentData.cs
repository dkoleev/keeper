using System;
using Avocado.Game.Components;
using JetBrains.Annotations;

namespace Avocado.Game.Data.Components {
    [Serializable]
    [UsedImplicitly]
    [ComponentType(ComponentType.AI)]
    public readonly struct AiComponentData : IComponentData {
        public readonly string AiType;

        public AiComponentData(string aiType) {
            AiType = aiType;
        }
    }
}