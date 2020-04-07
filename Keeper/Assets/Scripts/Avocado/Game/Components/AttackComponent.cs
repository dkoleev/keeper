using Avocado.Game.Behaviuor;
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
        public WeaponComponent WeaponComponent { get; }
        public int StartAmmo => Data.StartAmmo;
        
        private Entity _currentWeapon { get; }

        public AttackComponent(Entity entity, AttackComponentData data) : base(entity, data) {
            if (!string.IsNullOrEmpty(Data.Weapon)) {
                var weaponParent = Entity.gameObject.GetComponentInChildren<WeaponPlacer>();
                _currentWeapon = Entity.Create(Entity.GameData, Data.Weapon, Vector3.zero, weaponParent.transform);
                _currentWeapon.transform.position = Vector3.zero;
                _currentWeapon.transform.rotation = Quaternion.identity;
                WeaponComponent = World.GetComponentForEntity<WeaponComponent>(_currentWeapon);
            }
        }
    }
}