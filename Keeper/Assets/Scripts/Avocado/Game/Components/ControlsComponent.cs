using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.PlayerControls)]
    public class ControlsComponent : IComponent
    {
        public Entity Entity { get; }

        private readonly PlayerControlsComponentData _data;

        public ControlsComponent(Entity entity, IComponentData data)
        {
            Entity = entity;
            _data = (PlayerControlsComponentData) data;
        }
    }
}