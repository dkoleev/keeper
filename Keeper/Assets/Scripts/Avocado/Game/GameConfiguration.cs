using Avocado.Core.Loader;
using Avocado.Game.Data;

namespace Avocado.Game {
    public class GameConfiguration {
        private const string DataPath = "GameData/";

        private GameData _data;

        public GameData Load(ILoader loader)
        {
            _data = new GameData
            {
                Entities = loader.LoadObject<EntitiesData>(DataPath + "Entities.json")
            };

            return _data;
        }
    }
}