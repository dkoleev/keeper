using System.Collections.Generic;
using Avocado.Framework.Patterns.EventSystem;
using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Events;
using Avocado.Game.Worlds;
using Unity.Collections;
using UnityEngine;
using Unity.Jobs;

namespace Avocado.Game.Systems {
    public class AttackSystem : BaseSystem {
        private readonly int _attackAnimationKey = Animator.StringToHash("Attack");
        private List<(AttackComponent attackComponent, MoveComponent moveComponent)> _components = new List<(AttackComponent, MoveComponent)>();
        private List<HealthComponent> _targets = new List<HealthComponent>();

        public AttackSystem(GameData data) : base(data) { }
        
        public override void Initialize() {
            _components = World.GetComponents<AttackComponent, MoveComponent>();
            _targets = World.GetComponents<HealthComponent>();

            EventSystem<ComponentsUpdatedEvent>.Subscribe(coEvent => {
                _components = World.GetComponents<AttackComponent, MoveComponent>();
                _targets = World.GetComponents<HealthComponent>();
            });
        }

        public override void Update() {
            foreach (var componentTuple in _components) {
                var fireAttackComponent = componentTuple.attackComponent.WeaponComponent;

                var isMoving = componentTuple.moveComponent.CurrentSpeedMove > 0;
                if (!isMoving) {
                    foreach (var target in _targets) {
                        if (componentTuple.moveComponent.Entity != target.Entity) {
                            if (Vector3.Distance(componentTuple.moveComponent.Entity.transform.position, target.Entity.transform.position) <= componentTuple.attackComponent.WeaponComponent.Range) {

                                if (!fireAttackComponent.IsAttack) {
                                    fireAttackComponent.IsAttack = true;
                                    componentTuple.attackComponent.Entity.Animator.SetBool(_attackAnimationKey, fireAttackComponent.IsAttack);
                                }

                                break;
                            }

                            if (!fireAttackComponent.IsAttack) {
                                continue;
                            }

                            fireAttackComponent.IsAttack = false;
                            componentTuple.attackComponent.Entity.Animator.SetBool(_attackAnimationKey,  fireAttackComponent.IsAttack);
                        }
                    }
                } else if (fireAttackComponent.IsAttack) {
                    fireAttackComponent.IsAttack = false;
                    componentTuple.attackComponent.Entity.Animator.SetBool(_attackAnimationKey, fireAttackComponent.IsAttack);
                }
            }
        }
    }
}