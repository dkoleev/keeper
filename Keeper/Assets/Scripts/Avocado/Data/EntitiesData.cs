using System;
using System.Collections.Generic;

namespace Avocado.Data {
    [Serializable]
    public class EntitiesData
    {
        public readonly Dictionary<string, EntityData> Entities;

        public EntitiesData(Dictionary<string, EntityData> entities)
        {
            Entities = entities;
        }
    }
}