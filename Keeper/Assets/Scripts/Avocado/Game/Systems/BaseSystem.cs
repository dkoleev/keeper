using Avocado.Game.Data;

namespace Avocado.Game.Systems {
    public abstract class BaseSystem {
        protected GameData Data;

        protected BaseSystem(GameData data) {
            Data = data;
        }

        public abstract void Initialize();
        public abstract void Update();
    }
}