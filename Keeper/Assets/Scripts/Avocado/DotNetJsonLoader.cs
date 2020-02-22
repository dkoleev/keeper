using System;
using Avocado.Core.Loader;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Avocado {
    public class DotNetJsonLoader : ILoader {
        public void LoadObject<T>(string path, Action<T> onLoad) {
            string filePath = path.Replace(".json", "");
            Addressables.LoadAssetAsync<TextAsset>(filePath).Completed += handle => {
                var res = JsonConvert.DeserializeObject<T>(handle.Result.text);
                onLoad.Invoke(res);
            };
        }
    }
}