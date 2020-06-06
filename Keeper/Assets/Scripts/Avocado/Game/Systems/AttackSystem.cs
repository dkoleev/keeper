using System.Collections.Generic;
using Avocado.Framework.Patterns.EventSystem;
using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Events;
using Avocado.Game.Worlds;
using UnityEngine;

namespace Avocado.Game.Systems {
    public class AttackSystem : BaseSystem {
        private readonly int _animatorConditionId = Animator.StringToHash("ID");
        private readonly int _animatorMode = Animator.StringToHash("Mode");
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
                        if (componentTuple.moveComponent.Entity != target.Entity &&
                            !(componentTuple.attackComponent.WeaponComponent is null)) {
                            if (Vector3.Distance(componentTuple.moveComponent.Entity.transform.position, target.Entity.transform.position) <= componentTuple.attackComponent.WeaponComponent.Range) {
                                if (!fireAttackComponent.IsAttack) {
                                    fireAttackComponent.IsAttack = true;
                                    componentTuple.attackComponent.Entity.RotateTransform.LookAt(target.Entity.transform);
                                }

                                break;
                            }

                            if (!fireAttackComponent.IsAttack) {
                                continue;
                            }

                            fireAttackComponent.IsAttack = false;
                        }
                    }
                } else if (fireAttackComponent.IsAttack) {
                    fireAttackComponent.IsAttack = false;
                }

                if (fireAttackComponent is null) {
                    return;
                }

                if (fireAttackComponent.IsAttack) {
                    if (fireAttackComponent.CurrentDelay <= 0) {
                        fireAttackComponent.CurrentDelay = fireAttackComponent.Delay;
                        Shoot(componentTuple.attackComponent);
                    }

                    fireAttackComponent.CurrentDelay -= Time.deltaTime;
                } 

                componentTuple.attackComponent.Entity.Animator.SetInteger(_animatorConditionId,  fireAttackComponent.IsAttack ? 1 : 0);
                componentTuple.attackComponent.Entity.Animator.SetInteger(_animatorMode,  fireAttackComponent.IsAttack ? 101 : 100);
            }
        }

        private void Shoot(AttackComponent attack) {
            attack.Entity.Animator.SetInteger(_animatorConditionId, 2);
        }
    }
}