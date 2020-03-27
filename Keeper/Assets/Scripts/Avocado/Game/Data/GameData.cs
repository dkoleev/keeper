using System;

namespace Avocado.Game.Data
{
    [Serializable]
    public readonly struct GameData
    {
        public readonly EntitiesData Entities;

        public GameData(EntitiesData entities)
        {
            Entities = entities;
        }
    }
}