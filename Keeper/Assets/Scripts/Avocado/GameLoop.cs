using Avocado.Core;
using Avocado.Models.Worlds;

namespace Avocado {
    public class GameLoop : MonoBehaviourWrapper {
        private World _world;
        public void Initialize(World world) {
            _world = world;
        }

        protected override void Update() {
            var entities = _world.Entities.ToArray();
            foreach (var entity in entities) {
                entity.Update();
            }
        }
    }
}