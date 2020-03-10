using System;
using System.Collections.Generic;

namespace Avocado.Game.Data
{
    [Serializable]
    public class EntityData {
        public string Parent;
        public string Prefab;
        public Dictionary<string, ComponentData> Components;
    }
}