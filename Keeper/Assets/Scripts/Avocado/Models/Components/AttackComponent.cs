using System.Collections.Generic;
using Avocado.Framework.Patterns.Factory;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Data.Components;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using Sigtrap.Relays;
using UnityEngine;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Attack)]
    public class AttackComponent : ComponentBase<AttackComponentData> {
        public Vector3 CurrentPosition { get; private set; }
        public Entity CurrentWeapon => _currentWeapon;
        public bool IsAttack => WeaponComponent.IsAttack;
        public bool IsMoving => _moveComponent.IsMoving;
        public Entity CurrentTarget => _currentTarget.entity;
        
        public Relay OnShoot = new Relay();
        public Relay OnMove= new Relay();
        
        private List<Entity> _targets = new List<Entity>();

        public WeaponComponent WeaponComponent { get; private set; }
        public int StartAmmo => Data.StartAmmo;

        private Entity _currentWeapon;
        private MoveComponent _moveComponent;
        private (Entity entity, HealthComponent health) _currentTarget;

        public AttackComponent(string type, Entity entity, AttackComponentData data) : base(type, entity, data) {    
            
        }

        public override void Initialize() {
            base.Initialize();
            
            if (!string.IsNullOrEmpty(Data.Weapon)) {
                _currentWeapon = Entity.World.CreateEntity(Data.Weapon, Entity);
                WeaponComponent = (WeaponComponent) _currentWeapon.GetComponentByType<WeaponComponent>();
            }

            _moveComponent = (MoveComponent)Entity.GetComponentByType<MoveComponent>();

            _targets = Entity.World.GetEntitiesWithComponent<HealthComponent>();
            Entity.World.OnEntityCreate.AddListener(entity => {
                var health = entity.GetComponentByType<HealthComponent>();
                if (health != null && !_targets.Contains(entity)) {
                    _targets.Add(entity);
                }
            });

            Initialized = true;
        }

        public override void Update() {
            if (!Initialized) {
                return;
            }
            
            if (WeaponComponent is null) {
                return;
            }

            var isMoving = _moveComponent.CurrentSpeedMove > 0.01f;
            if (!isMoving) {
                if (_currentTarget.entity != null) {
                    if (!_currentTarget.health.IsAlive) {
                        if (_targets.Contains(_currentTarget.entity)) {
                            _targets.Remove(_currentTarget.entity);
                        }
                        _currentTarget.entity = null;
                        WeaponComponent.IsAttack = false;
                    } else {
                        if (CanShoot(_currentTarget.entity)) {
                            TryShoot();
                            return;
                        } 
                    
                        _currentTarget.entity = null;
                        WeaponComponent.IsAttack = false;
                    }
                }

                var targetsBuffer = _targets.ToArray();
                foreach (var target in targetsBuffer) {
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
                if (!_currentTarget.health.Initialized) {
                    return;
                }

                _currentTarget.health.Damage(WeaponComponent.Damage);
                OnShoot.Dispatch();
            }
        }

        private bool CanShoot(Entity target) {
            return Vector3.Distance(Entity.Position, target.Position) <= WeaponComponent.Range;
        }
    }
}