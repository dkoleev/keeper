using Avocado.Game.Components.Weapons;
using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using Avocado.Game.Worlds;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Attack)]
    public class AttackComponent : ComponentBase<AttackComponentData> {
        public Entity CurrentWeapon { get; }
        public FireAttackComponent FireAttackComponent { get; }

        public AttackComponent(Entity entity, AttackComponentData data) : base(entity, data) {
            if (!string.IsNullOrEmpty(Data.CurrentWeapon)) {
                CurrentWeapon = Entity.Create(Entity.GameData, Data.CurrentWeapon);
                FireAttackComponent = World.GetComponentForEntity<FireAttackComponent>(CurrentWeapon);
            }
        }
    }
}