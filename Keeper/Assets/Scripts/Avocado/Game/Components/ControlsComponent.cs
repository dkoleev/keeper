using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.PlayerControls)]
    public class ControlsComponent : ComponentBase<PlayerControlsComponentData>
    {
        public ControlsComponent(Entity entity, PlayerControlsComponentData data) : base(entity, data) { }
    }
}