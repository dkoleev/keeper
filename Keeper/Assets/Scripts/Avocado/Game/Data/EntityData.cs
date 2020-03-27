using System;
using System.Collections.Generic;

namespace Avocado.Game.Data
{
    [Serializable]
    public readonly struct EntityData
    {
        public readonly string Parent;
        public readonly string Prefab;
        public readonly Dictionary<ComponentType, IComponentData> Components;

        public EntityData(string parent, string prefab, Dictionary<ComponentType, IComponentData> components)
        {
            Parent = parent;
            Prefab = prefab;
            Components = components;
        }
    }
}