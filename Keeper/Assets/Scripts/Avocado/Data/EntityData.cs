using System;
using System.Collections.Generic;

namespace Avocado.Data {
    [Serializable]
    public class EntityData {
        public readonly string Parent;
        public readonly string Prefab;
        public readonly Dictionary<string, IComponentData> Components;

        public EntityData(string parent, string prefab, Dictionary<string, IComponentData> components) {
            Parent = parent;
            Prefab = prefab;
            Components = components;
        }
    }
}