using System;
using System.Collections.Generic;
using Avocado.Game.Components;

namespace Avocado.Game.Data
{
    [Serializable]
    public class EntityData {
        public string Parent;
        public string Prefab;
        public Dictionary<ComponentType, IComponentData> Components;
    }
}