using System.Collections.Generic;
using Avocado.Data;
using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using Sigtrap.Relays;
using UnityEngine;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Attack)]
    public class AttackComponent : ComponentBase<AttackComponentData> {
        public Vector3 CurrentPosition { get; private set; }
        public Entity CurrentWeapon => _currentWeapon;
        public bool IsAttack => WeaponComponent.IsAttack;
        public Entity CurrentTarget => _currentTarget.entity;
        
        public Relay OnShoot = new Relay();
        
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
                _currentWeapon = Entity.World.CreateEntity(Data.Weapon, Entity);
                WeaponComponent = (WeaponComponent) _currentWeapon.GetComponentByType<WeaponComponent>();
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
                OnShoot.Dispatch();
            }
        }

        private bool CanShoot(Entity target) {
            return Vector3.Distance(Entity.Position, target.Position) <= WeaponComponent.Range;
        }
    }
}