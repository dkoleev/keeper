using Avocado.Framework.Patterns.AbstractFactory;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ObjectType("Damage")]
    public struct DamageComponent : IComponent {
        public Entity Entity { get; private set; }
        public bool Initialized { get; private set; }
        public void Initialize(Entity entity)
        {
            Entity = entity;
            Initialized = true;
        }
    }
}
