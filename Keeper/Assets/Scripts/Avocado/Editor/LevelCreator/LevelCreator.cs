using System.Collections.Generic;
using System.IO;
using Avocado.Core.Loader.Variants;
using Avocado.Data;
using Avocado.Data.Levels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Avocado.Editor.LevelCreator {
    public class LevelCreator {
        public GameData GameData => _gameData;
        
        private LevelsData _data;
        private const string LevelScenesPath = "Assets/Scenes/DudesInDungeons/";
        private const string FilePath = "/Configuration/Resources/" + FileName + ".json";
        private const string FileName = "LevelsData";
        private Scene _currentLevel;
        private GameData _gameData;
        
        public void SaveData() {
            var result = JsonConvert.SerializeObject(_data, Formatting.Indented,
                new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.Auto
                });

            File.WriteAllText(Application.dataPath + FilePath, result);
            AssetDatabase.Refresh();
        }
        
        public void LoadData() {
            LoadLevelsData();
            LoadGameData();
        }

        private void LoadLevelsData() {
            if (!File.Exists(Application.dataPath + FilePath)) {
                var fs = new FileStream(Application.dataPath + FilePath, FileMode.Create);
                fs.Dispose();
            }

            var asset = Resources.Load<TextAsset>(FileName);

            _data = JsonConvert.DeserializeObject<LevelsData>(asset.text, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = new List<JsonConverter> {
                    new StringEnumConverter()
                }
            });
        }

        private void LoadGameData() {
            var loader = new DotNetJsonLoader();
            var config = new GameConfiguration();
            _gameData = config.Load(loader);
        }

        public IReadOnlyList<FileInfo> GetLevels() {
            var levels = new List<FileInfo>();
            var dir = new DirectoryInfo(LevelScenesPath);
            var directories = dir.GetDirectories();
            foreach (var directory in directories) {
                FileInfo[] info = directory.GetFiles("*.unity");
                foreach (FileInfo f in info) {
                    levels.Add(f);
                }
            }

            return levels;
        }
        
        public void OpenScene(string sceneName) {
            ClearScene();
            _currentLevel = EditorSceneManager.OpenScene(sceneName, OpenSceneMode.Additive);
        }

        public void ClearScene() {
            EditorSceneManager.CloseScene(_currentLevel, true);

            var allGarbage = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (var garbage in allGarbage) {
                var so = garbage.GetComponent<SceneObjectEditor>();
                if (so != null) {
                    Object.DestroyImmediate(garbage);
                }
            }
        }
    }
}
