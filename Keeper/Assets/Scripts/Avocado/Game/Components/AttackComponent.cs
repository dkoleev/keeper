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
        private readonly int _attackAnimationKey = Animator.StringToHash("Attack");
        private IReadOnlyList<Entity> _targets = new List<Entity>();

        public WeaponComponent WeaponComponent { get; private set; }
        public int StartAmmo => Data.StartAmmo;

        private Entity _currentWeapon;
        private MoveComponent _moveComponent;
        private Entity _currentTarget;

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
            if (WeaponComponent is null) {
                return;
            }

            var isMoving = _moveComponent.CurrentSpeedMove > 0.01f;
            if (!isMoving) {
                if (_currentTarget != null) {
                    if (CanShoot(_currentTarget)) {
                        Entity.RotateTransform.LookAt(_currentTarget.transform);
                        TryShoot();
                        return;
                    } 
                    
                    _currentTarget = null;
                    WeaponComponent.IsAttack = false;
                    Update();
                }

                foreach (var target in _targets) {
                    if (_moveComponent.Entity != target &&
                        !(WeaponComponent is null)) {
                        if (CanShoot(target)) {
                            _currentTarget = target;
                            WeaponComponent.IsAttack = true;
                            Entity.Animator.SetTrigger(_attackAnimationKey);
                        }
                    }
                }
            } else {
                _currentTarget = null;
                WeaponComponent.IsAttack = false;
            }

            void TryShoot() {
                if (WeaponComponent.IsAttack) {
                    if (WeaponComponent.CurrentDelay <= 0) {
                        WeaponComponent.CurrentDelay = WeaponComponent.Delay;
                        Shoot(this);
                    }

                    WeaponComponent.CurrentDelay -= Time.deltaTime;
                }
            }

            /*Entity.Animator.SetInteger(_animatorConditionId, WeaponComponent.IsAttack ? 1 : 0);
            Entity.Animator.SetInteger(_animatorMode, WeaponComponent.IsAttack ? 101 : 100);*/
        }

        private bool CanShoot(Entity target) {
            return Vector3.Distance(Entity.transform.position, target.transform.position) <= WeaponComponent.Range;
        }

        public bool IsAttack() {
            if (WeaponComponent == null) {
                return false;
            }

            return WeaponComponent.IsAttack;
        }

        private void Shoot(AttackComponent attack) {
           // attack.Entity.Animator.SetInteger(_animatorConditionId, 2);
        }
    }
}