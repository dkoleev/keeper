using System.Collections.Generic;
using Avocado.Framework.Patterns.EventSystem;
using Avocado.Game.Data;
using Avocado.Game.Events;
using Avocado.Game.Systems;
using UnityEngine;

namespace Avocado.Game {
    [DisallowMultipleComponent]
    public class GameRunner : MonoBehaviourWrapper {
        private List<BaseSystem> _systems;
        
        protected override void Start()
        {
            base.Start();
            
            Load();
            var goLoop = new GameObject("GameLoop");
            goLoop.AddComponent<GameLoop>();
        }

        private void Load() {
            LoadGameState();
            var gameData = LoadConfiguration();
            LoadSystems(gameData);

            EventSystem<PlayerDeadEvent>.Subscribe(data => {
                Debug.Log("fire event " + data.LiveTime);
            });

            var playerSystem = GetSystem<PlayerSystem>();
            playerSystem.Dead();
        }

        private GameData LoadConfiguration() {
            var loader = new DotNetJsonLoader();
            var config = new GameConfiguration();
            return  config.Load(loader);
        }

        private void LoadGameState() {
            
        }

        private void LoadSystems(GameData gameData)
        {
            _systems = new List<BaseSystem> {
                new PlayerSystem(gameData),
                new ComponentsSystem(gameData)
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