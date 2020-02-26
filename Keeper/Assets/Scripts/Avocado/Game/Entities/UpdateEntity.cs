using Avocado.Game.Systems;

namespace Avocado.Game.Entities {
    public class UpdateEntity : Entity {
        private UpdateSystem _system;
        
        public void SetupSystem(UpdateSystem system) {
            _system = system;
        }

        private void Update() {
            _system.Update();
        }
    }
}