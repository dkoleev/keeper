using Avocado.Core;
using Avocado.Core.Loader.Variants;
using Avocado.Core.SaveEngine;
using Avocado.Data;
using Avocado.Game.Core;
using Avocado.Models.Worlds;
using Avocado.ModelViews;
using Avocado.Progress;
using UnityEngine;

namespace Avocado {
    [DisallowMultipleComponent]
    public class GameRunner : MonoBehaviourWrapper, IInitializable {
        public bool Initialized { get; private set; }
        private Transform _playerSpawnPosition;
        private Transform _worldSize;
        private SaveEngine<GameProgress> _saveEngine;

        protected override void Start() {
            base.Start();
            Initialize();
        }

        public void Initialize() {
            _playerSpawnPosition = GameObject.FindWithTag("PlayerSpawnPosition").transform;
            _worldSize = GameObject.FindWithTag("WorldBounds").transform;

            Load(out var world);
            
            var goLoop = new GameObject("GameLoop");
            var gameLoop = goLoop.AddComponent<GameLoop>();
            gameLoop.Initialize(world);

            Initialized = true;
        }
        
        private void Load(out World world) {
            var gameProgress = LoadGameState();
            var gameData = LoadConfiguration();
            LoadWorld(gameData, gameProgress, out world);
            LoadWorldView(world);
        }

        protected override void Update()
        {
            base.Update();

            if (!Initialized)
            {
                return;
            }
        }
        
        private GameData LoadConfiguration() {
            var loader = new DotNetJsonLoader();
            var config = new GameConfiguration();
            return  config.Load(loader);
        }

        private GameProgress LoadGameState() {
            _saveEngine = new SaveEngine<GameProgress>(new GameProgress());
            return _saveEngine.LoadProgress() as GameProgress;
        }

        private void LoadWorld(GameData data, GameProgress gameProgress, out World world) {
           LoadModels(data, gameProgress, out world);
        }

        private void LoadWorldView(World world) {
            LoadModelViews(world);
        }

        private void LoadModels(GameData data, GameProgress gameProgress, out World world) {
            world = new World(data, gameProgress);
            world.Create(_worldSize.localScale, _playerSpawnPosition.position);
        }

        private void LoadModelViews(World world) {
            var worldView = new WorldView(world);
            worldView.Create();
        }

        private void OnApplicationQuit() {
            _saveEngine?.SaveProgress();
        }
    }
}