using System;

namespace Avocado.Data
{
    [Serializable]
    public class GameData
    {
        public readonly EntitiesData Entities;

        public GameData(EntitiesData entities)
        {
            Entities = entities;
        }
    }
}