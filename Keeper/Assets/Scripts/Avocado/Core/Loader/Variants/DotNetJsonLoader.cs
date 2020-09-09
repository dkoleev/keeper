using Newtonsoft.Json;
using UnityEngine;

namespace Avocado.Core.Loader.Variants {
    public class DotNetJsonLoader : ILoader {
        public T LoadObject<T>(string path) {
            string filePath = path.Replace(".json", "");

            TextAsset targetFile = Resources.Load<TextAsset>(filePath);

            var res = JsonConvert.DeserializeObject<T>(targetFile.text);

            return res;
        }
    }
}