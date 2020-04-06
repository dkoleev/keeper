using Avocado.Game.Behaviuor;
using Avocado.Game.Components.Weapons;
using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using Avocado.Game.Worlds;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Attack)]
    public class AttackComponent : ComponentBase<AttackComponentData> {
        public Entity CurrentWeapon { get; }
        public WeaponComponent WeaponComponent { get; }
        public int StartAmmo => Data.StartAmmo;

        public AttackComponent(Entity entity, AttackComponentData data) : base(entity, data) {
            if (!string.IsNullOrEmpty(Data.Weapon)) {
                var weaponParent = Entity.gameObject.GetComponentInChildren<WeaponPlacer>();
                CurrentWeapon = Entity.Create(Entity.GameData, Data.Weapon, Vector3.zero, weaponParent.transform);
                CurrentWeapon.transform.position = Vector3.zero;
                CurrentWeapon.transform.rotation = Quaternion.identity;
                WeaponComponent = World.GetComponentForEntity<WeaponComponent>(CurrentWeapon);
            }
        }
    }
}