using System.Collections.Generic;
using Avocado.Framework.Patterns.EventSystem;
using Avocado.Game.Data;
using Avocado.Game.Events;
using Avocado.Game.Systems;
using Avocado.Game.Worlds;
using UnityEngine;

namespace Avocado.Game {
    [DisallowMultipleComponent]
    public class GameRunner : MonoBehaviourWrapper {
        private List<BaseSystem> _systems;
        private World _world;

        private bool _initialized;
        
        protected override void Start()
        {
            base.Start();
            
            Load();
            var goLoop = new GameObject("GameLoop");
            goLoop.AddComponent<GameLoop>();
        }

        protected override void Update()
        {
            base.Update();

            if (!_initialized)
            {
                return;
            }

            foreach (var system in _systems)
            {
                system.Update();
            }
        }

        private void Load() {
            LoadGameState();
            var gameData = LoadConfiguration();
            LoadWorld(gameData);
            LoadSystems(gameData);

            EventSystem<PlayerDeadEvent>.Subscribe(data => {
                Debug.Log("fire event " + data.LiveTime);
            });

            var playerSystem = GetSystem<PlayerSystem>();
            playerSystem.Dead();

            _initialized = true;
        }

        private GameData LoadConfiguration() {
            var loader = new DotNetJsonLoader();
            var config = new GameConfiguration();
            return  config.Load(loader);
        }

        private void LoadGameState() {
            
        }

        private void LoadWorld(GameData gameData)
        {
            _world = new World(gameData);
        }

        private void LoadSystems(GameData gameData)
        {
            _systems = new List<BaseSystem> {
                new MoveByControlsSystem(gameData),
                new PlayerSystem(gameData),
                new AttackSystem(gameData),
                new SpawnSystem(gameData)
            };

            foreach (var system in _systems) {
                system.Initialize();
            }
        }

        private TSystem GetSystem<TSystem>() where TSystem : class {
            foreach (var system in _systems) {
                if (system is TSystem system1) {
                    return system1;
                }
            }

            return null;
        }
    }
}