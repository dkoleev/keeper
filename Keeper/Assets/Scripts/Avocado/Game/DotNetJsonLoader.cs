using Avocado.Core.Loader;
using Avocado.Game.Data;
using Avocado.Game.Utilities;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using UnityEngine;

namespace Avocado.Game {
    public class DotNetJsonLoader : ILoader {
        public T LoadObject<T>(string path)
        {
            string filePath = path.Replace(".json", "");

            TextAsset targetFile = Resources.Load<TextAsset>(filePath);
            
            var compon = new ComponentData();
            compon.Type = "Test";
            string json = JsonConvert.SerializeObject(compon, Formatting.Indented);

            AvocadoLogger.Log(json);

            var res = JsonConvert.DeserializeObject<T>(targetFile.text);

            return res;
        }
    }
}