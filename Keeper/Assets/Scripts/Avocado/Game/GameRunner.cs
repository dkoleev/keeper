using UnityEngine;

namespace Avocado.Game {
    [DisallowMultipleComponent]
    public class GameRunner : MonoBehaviourWrapper
    {
        protected override void Start()
        {
            base.Start();
            
            Load();
        }

        private void Load() {
            LoadConfiguration();
            LoadGameState();
        }

        private void LoadConfiguration() {
            var loader = new DotNetJsonLoader();
            var config = new GameConfiguration();
            config.Load(loader);
        }

        private void LoadGameState() {
            
        }

        private void InitializeSystems()
        {
            
        }
    }
}