using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Avocado.Editor.LevelCreator {
    public class LevelCreatorWindow : EditorWindow {
        private LevelCreatorPreferences _preference;
        private bool _autoSave;
        private Scene _currentLevel;
        private LevelCreator _levelCreator;
        private int _selectedLevelIndex = -1;
        private IReadOnlyList<FileInfo> _levels;
        private IReadOnlyList<string> _levelNames;
        
        private Vector2 _scrollView = Vector2.zero;
        private readonly List<PreviewObject> _previewSceneObjects = new List<PreviewObject>();

        private bool _initialized;
        
        [MenuItem("Tools/Level Creator")]
        public static void ShowWindow() {
            var window = (LevelCreatorWindow) GetWindow(typeof(LevelCreatorWindow));
            window.titleContent = new GUIContent("Level Creator");
            window.Show();
        }
        
        private void OnEnable() {
            _levelCreator = new LevelCreator();
            _levelCreator.LoadData();
            _levels = _levelCreator.GetLevels();
            _levelNames = _levels.Select(info => info.Name.Replace(".unity", "")).ToList();

            LevelCreatorHelper.InitIcons();
            titleContent = new GUIContent("Scene Editor");
            minSize = new Vector2(325, 100);
            _preference = new LevelCreatorPreferences();
            _preference.LoadPreference();
            _autoSave = _preference.autoSave;

            PrepareStartWorkspace();
            SceneView.duringSceneGui += SceneGui;

            _initialized = true;
        }

        private void OnDisable() {
            _levelCreator.ClearScene();
            SceneView.duringSceneGui -= SceneGui;
        }

        private void PrepareStartWorkspace() {
            LoadEntities();
        }

        private void LoadEntities() {
            var aaSettings = AddressableAssetSettingsDefaultObject.Settings;
            foreach (var item in _levelCreator.GameData.Entities.Entities) {
                var entries = from addressableAssetGroup in aaSettings.groups
                    from entrie in addressableAssetGroup.entries
                    where entrie.address == item.Value.Prefab
                    select entrie;

                foreach (var entrie in entries) {
                    var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(entrie.AssetPath);
                    if (prefab == null) {
                        Debug.LogError($"Can't find prefab for {item.Key}");
                    } else {
                        CreateSceneObjectPreview(prefab, item.Key);
                    }
                }
            }
        }
        
        private void SceneGui(SceneView sceneView) {
            sceneView.Repaint();
        }

        private void OnGUI() {
            if (!_initialized) {
                return;
            }

            DrawLevelSelector();
            DrawEntities();
        }

        private void DrawLevelSelector() {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            _selectedLevelIndex = EditorGUILayout.Popup("", _selectedLevelIndex, _levelNames.ToArray(), GUILayout.Height(25));
            if (GuiTools.Button("Open", Color.green, 100, 20)) {
                if (_selectedLevelIndex != -1) {
                    _levelCreator.OpenScene(_levels[_selectedLevelIndex].FullName);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawEntities() {
            var selectedOptions = new List<string>();
            foreach (var entity in _levelCreator.GameData.Entities.Entities) {
                selectedOptions.Add(entity.Key);
            }

            EditorGUILayout.Space();
            _scrollView = EditorGUILayout.BeginScrollView(_scrollView);
            GuiTools.BeginGroup();
            DrawPrefabInspector(selectedOptions);
            GuiTools.EndGroup();
            EditorGUILayout.EndScrollView();
        }
        
        private void DrawPrefabInspector(List<string> selectedTypes) {
            EditorGUILayout.Space();
            for (int i = 0; i < _previewSceneObjects.Count; i++) {
                if (_previewSceneObjects[i].prefab != null) {
                    DrawPrefabChild(_previewSceneObjects[i]);
                } else {
                    _previewSceneObjects.Remove(_previewSceneObjects[i]);
                }
            }
        }
        
        private void DrawPrefabChild(PreviewObject scatterChild) {
            EditorGUILayout.BeginHorizontal();
            bool tmpButtonState = GuiTools.ToggleButton(scatterChild.enable, scatterChild, ref scatterChild.preview, scatterChild.prefab);
            if (!scatterChild.enable && tmpButtonState) {
                scatterChild.enable = true;
                List<PreviewObject> tempObj = _previewSceneObjects.FindAll(s => s.enable == true && s != scatterChild);

                foreach (PreviewObject sco in tempObj) {
                    sco.enable = false;
                }
            } else {
                scatterChild.enable = tmpButtonState;
            }

            EditorGUILayout.EndHorizontal();
        }

        private void CreateSceneObjectPreview(Object dragObject, string soKey) {
            if (dragObject is GameObject || dragObject.GetType() == typeof(Transform)) {
                GameObject obj = null;
                if (dragObject is GameObject gameObject) {
                    obj = gameObject;
                } else {
                    obj = ((Transform) dragObject).gameObject;
                }

                var result = _previewSceneObjects.FindIndex(s => s.SceneObjectKey == soKey);
                if (result == -1) {
                    var so = new PreviewObject();
                    so.prefab = obj;
                    so.SceneObjectKey = soKey;
                    so.enable = false;
                    so.isPrefab = PrefabUtility.IsPartOfPrefabAsset(obj);
                    so.offset = 0f;
                    
                    _previewSceneObjects.Add(so);
                }
            }
        }
    }
}