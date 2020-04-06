using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Attack)]
    public class AttackComponent : ComponentBase<AttackComponentData> {
        public Entity CurrentWeapon { get; }

        public AttackComponent(Entity entity, AttackComponentData data) : base(entity, data) {
            //TODO: load current weapon
        }
    }
}