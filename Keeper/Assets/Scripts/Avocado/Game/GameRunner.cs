using System.Collections.Generic;
using Avocado.Game.Data;
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
        }

        private void Load() {
            LoadGameState();
            var gameData = LoadConfiguration();
            LoadSystems(gameData);
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
                new PlayerSystem(gameData)
            };

            foreach (var system in _systems) {
                system.Initialize();
            }
        }
    }
}