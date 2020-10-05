using Avocado.Models.Entities;

namespace Avocado.Models.Components {
    public interface IComponent {
        string Type { get; }
        Entity Entity { get; }
        bool Initialized { get; }
        void Initialize();
        void Update();
    }
}
