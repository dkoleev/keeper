using System.Collections.Generic;
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
        private readonly int _animatorConditionId = Animator.StringToHash("ID");
        private readonly int _animatorMode = Animator.StringToHash("Mode");
        private IReadOnlyList<Entity> _targets = new List<Entity>();

        public WeaponComponent WeaponComponent { get; private set; }
        public int StartAmmo => Data.StartAmmo;

        private Entity _currentWeapon;
        private MoveComponent _moveComponent;

        public AttackComponent(Entity entity, AttackComponentData data) : base(entity, data) {
         
        }

        public override void Initialize() {
            base.Initialize();
            if (!string.IsNullOrEmpty(Data.Weapon)) {
                var weaponParent = Entity.gameObject.GetComponentInChildren<WeaponPlacer>();
                World.CreateEntity(Data.Weapon, Vector3.zero, weaponParent.transform,
                    weaponEntity => {
                        _currentWeapon = weaponEntity;
                        var transform = _currentWeapon.transform;
                        transform.localPosition = Vector3.zero;
                        transform.localRotation = Quaternion.identity;
                        WeaponComponent = (WeaponComponent) _currentWeapon.GetComponentByType<WeaponComponent>();
                    });
            }

            _moveComponent = (MoveComponent)Entity.GetComponentByType<MoveComponent>();
            _targets = World.GetEntitiesWithComponent<HealthComponent>();
        }

        public override void Update() {
                var isMoving = _moveComponent.CurrentSpeedMove > 0;
                if (!isMoving) {
                    foreach (var target in _targets) {
                        if (_moveComponent.Entity != target &&
                            !(WeaponComponent is null)) {
                            if (Vector3.Distance(Entity.transform.position, target.transform.position) <= WeaponComponent.Range) {
                                if (!WeaponComponent.IsAttack) {
                                    WeaponComponent.IsAttack = true;
                                    Entity.RotateTransform.LookAt(target.transform);
                                }

                                break;
                            }

                            if (!WeaponComponent.IsAttack) {
                                continue;
                            }

                            WeaponComponent.IsAttack = false;
                        }
                    }
                } else if (WeaponComponent.IsAttack) {
                    WeaponComponent.IsAttack = false;
                }

                if (WeaponComponent is null) {
                    return;
                }

                if (WeaponComponent.IsAttack) {
                    if (WeaponComponent.CurrentDelay <= 0) {
                        WeaponComponent.CurrentDelay = WeaponComponent.Delay;
                        Shoot(this);
                    }

                    WeaponComponent.CurrentDelay -= Time.deltaTime;
                }

                Entity.Animator.SetInteger(_animatorConditionId, WeaponComponent.IsAttack ? 1 : 0);
                Entity.Animator.SetInteger(_animatorMode, WeaponComponent.IsAttack ? 101 : 100);
        }

        private void Shoot(AttackComponent attack) {
            attack.Entity.Animator.SetInteger(_animatorConditionId, 2);
        }
    }
}