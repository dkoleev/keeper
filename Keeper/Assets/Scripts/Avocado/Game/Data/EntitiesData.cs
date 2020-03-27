using System;
using System.Collections.Generic;

namespace Avocado.Game.Data {
    [Serializable]
    public readonly struct EntitiesData
    {
        public readonly Dictionary<string, EntityData> Entities;

        public EntitiesData(Dictionary<string, EntityData> entities)
        {
            Entities = entities;
        }
    }
}