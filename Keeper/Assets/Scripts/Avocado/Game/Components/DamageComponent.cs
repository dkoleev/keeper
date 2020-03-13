using Avocado.Framework.Patterns.AbstractFactory;
using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ObjectType("Damage")]
    public struct DamageComponent : IComponent {
        public Entity Entity { get; private set; }

        private DamageComponentData _data;

        public void Initialize(Entity entity, IComponentData data) {
            Entity = entity;
            _data = (DamageComponentData) data;
        }

        public void Update() {
            
        }
    }
}
