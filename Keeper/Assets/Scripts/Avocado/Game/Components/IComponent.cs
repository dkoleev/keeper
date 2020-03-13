using Avocado.Game.Data;
using Avocado.Game.Entities;

namespace Avocado.Game.Components {
    public interface IComponent {
        Entity Entity { get; }
        void Initialize(Entity entity, IComponentData data);
        void Update();
    }
}
