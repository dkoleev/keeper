using System;

namespace Avocado.Game.Data
{
    [Serializable]
    public class EntityData {
        public string Prefab;
        public ComponentData[] Components;
    }
}