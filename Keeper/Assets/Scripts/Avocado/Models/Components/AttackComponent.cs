using System.Collections.Generic;
using Avocado.Game.Behaviuor;
using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.Models.Components {
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
        private (Entity entity, HealthComponent health) _currentTarget;

        public AttackComponent(Entity entity, AttackComponentData data) : base(entity, data) {
         
        }

        public override void Initialize() {
            base.Initialize();
            if (!string.IsNullOrEmpty(Data.Weapon)) {
                var weaponParent = Entity.gameObject.GetComponentInChildren<WeaponPlacer>();
                Entity.World.CreateEntity(Data.Weapon, Vector3.zero, weaponParent.transform,
                    weaponEntity => {
                        _currentWeapon = weaponEntity;
                        var transform = _currentWeapon.transform;
                        transform.localPosition = Vector3.zero;
                        transform.localRotation = Quaternion.identity;
                        WeaponComponent = (WeaponComponent) _currentWeapon.GetComponentByType<WeaponComponent>();
                    });
            }

            _moveComponent = (MoveComponent)Entity.GetComponentByType<MoveComponent>();
            _targets = Entity.World.GetEntitiesWithComponent<HealthComponent>();
        }

        public override void Update() {
            if (WeaponComponent is null) {
                return;
            }

            var isMoving = _moveComponent.CurrentSpeedMove > 0.01f;
            if (!isMoving) {
                if (_currentTarget.entity != null) {
                    if (CanShoot(_currentTarget.entity)) {
                        Entity.RotateTransform.LookAt(_currentTarget.entity.transform);
                        TryShoot();
                        return;
                    } 
                    
                    _currentTarget.entity = null;
                    WeaponComponent.IsAttack = false;
                    Update();
                }

                foreach (var target in _targets) {
                    if (_moveComponent.Entity != target &&
                        !(WeaponComponent is null)) {
                        if (CanShoot(target)) {
                            _currentTarget = (target, (HealthComponent)target.GetComponentByType<HealthComponent>());
                            WeaponComponent.IsAttack = true;
                            TryShoot();
                        }
                    }
                }
            } else {
                _currentTarget.entity = null;
                WeaponComponent.IsAttack = false;
            }

            void TryShoot() {
                if (WeaponComponent.IsAttack) {
                    if (WeaponComponent.CurrentDelay <= 0) {
                        WeaponComponent.CurrentDelay = WeaponComponent.Delay;
                        Shoot();
                    }

                    WeaponComponent.CurrentDelay -= Time.deltaTime;
                }
            }
            
            void Shoot() {
                _currentTarget.health.Damage(WeaponComponent.Damage);
                Entity.Animator.SetTrigger(_attackAnimationKey);
            }
        }

        private bool CanShoot(Entity target) {
            return Vector3.Distance(Entity.transform.position, target.transform.position) <= WeaponComponent.Range;
        }
    }
}