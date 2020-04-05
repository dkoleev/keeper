using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Damage)]
    public class DamageComponent : IComponent {
        public Entity Entity { get; }

        private readonly DamageComponentData _data;

        public DamageComponent(Entity entity, IComponentData data)
        {
            Entity = entity;
            _data = (DamageComponentData) data;
        }
    }
}
