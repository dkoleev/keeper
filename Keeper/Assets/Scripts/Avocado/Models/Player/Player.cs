using Avocado.Models.Worlds;
using Avocado.Progress;

namespace Avocado.Models.Player {
    public class Player {
        private PlayerProgress _progress;
        private World _world;
        
        public Player(PlayerProgress progress, World world) {
            _progress = progress;
            _world = world;
        }
    }
}