using Avocado.Game.Data;

namespace Avocado.Game.Systems {
    public abstract class BaseSystem {
        protected GameData Data;
        public BaseSystem(GameData data) {
            Data = data;
        }

        public void Initialize() {
            CreateActors();
        }

        protected abstract void CreateActors();
    }
}