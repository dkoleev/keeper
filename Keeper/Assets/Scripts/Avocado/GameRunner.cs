using Avocado.Core;
using Avocado.Core.Loader.Variants;
using Avocado.Game;
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
            Load();
            var goLoop = new GameObject("GameLoop");
            goLoop.AddComponent<GameLoop>();

            Initialized = true;
        }
        
        private void Load() {
            LoadGameState();
            var gameData = LoadConfiguration();
            LoadWorld(gameData);
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

        private void LoadWorld(GameData data) {
            LoadModeViews( LoadModels(data));
        }

        private World LoadModels(GameData data) {
            var world = new World(data);
            world.Create();

            return world;
        }

        private void LoadModeViews(World world) {
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