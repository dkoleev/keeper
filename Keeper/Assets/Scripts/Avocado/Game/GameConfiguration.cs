using Avocado.Core.Loader;
using Avocado.Game.Data;

namespace Avocado.Game {
    public class GameConfiguration {
        private const string DataPath = "GameData/";
        private GameData _data;

        public GameData Load(ILoader loader)
        {
            var entities = loader.LoadObject<EntitiesData>(DataPath + "Entities.json");
            _data = new GameData(entities);

            return _data;
        }
    }
}