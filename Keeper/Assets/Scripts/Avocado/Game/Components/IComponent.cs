using Avocado.Game.Entities;

namespace Avocado.Game.Components {
    public interface IComponent {
        Entity Entity { get; }
        bool Initialized { get; }
        void Initialize();
        void Update();
    }
}
