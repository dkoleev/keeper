using Avocado.Core;
using Avocado.Core.Loader.Variants;
using Avocado.Game.Core;
using Avocado.Game.Data;
using Avocado.Models.Worlds;
using Avocado.ModelViews;
using UnityEngine;

namespace Avocado {
    [DisallowMultipleComponent]
    public class GameRunner : MonoBehaviourWrapper, IInitializable {
        public bool Initialized { get; private set; }

        protected override void Start()
        {
            base.Start();
            Initialize();
        }
        
        public void Initialize() {
            Load(out var world);
            
            var goLoop = new GameObject("GameLoop");
            var gameLoop = goLoop.AddComponent<GameLoop>();
            gameLoop.Initialize(world);

            Initialized = true;
        }
        
        private void Load(out World world) {
            LoadGameState();
            var gameData = LoadConfiguration();
            LoadWorld(gameData, out world);
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

        private void LoadGameState() {
            
        }

        private void LoadWorld(GameData data, out World world) {
           LoadModels(data, out world);
        }

        private void LoadWorldView(World world) {
            LoadModelViews(world);
        }

        private void LoadModels(GameData data, out World world) {
            world = new World(data);
            world.Create();
        }

        private void LoadModelViews(World world) {
            var worldView = new WorldView(world);
            worldView.Create();
        }

        private void Test() {
            /*EventSystem<PlayerDeadEvent>.Subscribe(data => {
                Debug.Log("fire event " + data.LiveTime);
            });

            var playerSystem = GetSystem<PlayerSystem>();
            playerSystem.Dead();*/
        }
    }
}