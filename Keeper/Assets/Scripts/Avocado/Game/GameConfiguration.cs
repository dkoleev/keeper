using Avocado.Core.Loader;
using Avocado.Game.Data;

namespace Avocado.Game {
    public class GameConfiguration {
        private const string DataPath = "GameData/";

        public GameData Data { get; private set; }

        public void Load(ILoader loader)
        {
            Data = new GameData
            {
                Player = loader.LoadObject<PlayerData>(DataPath + "Player.json")
            };
        }
    }
}
