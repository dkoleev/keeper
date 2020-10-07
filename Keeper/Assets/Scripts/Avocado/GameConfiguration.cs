using System.Collections.Generic;
using Avocado.Core.Loader;
using Avocado.Data;

namespace Avocado {
    public class GameConfiguration {
        private const string DataPath = "GameData/";
        private GameData _data;

        public GameData Load(ILoader loader) {
            var resultEntities = new Dictionary<string, EntityData>();
            var main = loader.LoadObject<EntitiesData>(DataPath + "Humanoids.json");
            var weapons = loader.LoadObject<EntitiesData>(DataPath + "Weapons.json");
            
            AddToResult(resultEntities, main.Entities);
            AddToResult(resultEntities, weapons.Entities);
            
            var result = new EntitiesData(resultEntities);
            _data = new GameData(result);

            return _data;
        }

        private void AddToResult(Dictionary<string, EntityData> result, Dictionary<string, EntityData> newValues) {
            foreach (var newValue in newValues) {
                result.Add(newValue.Key, newValue.Value);
            }
        }
    }
}