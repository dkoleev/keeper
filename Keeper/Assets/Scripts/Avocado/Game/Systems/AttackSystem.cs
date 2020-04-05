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
        private List<(WeaponComponent weaponComponent, MoveComponent moveComponent)> _components = new List<(WeaponComponent, MoveComponent)>();
        private List<HealthComponent> _targets = new List<HealthComponent>();

        public AttackSystem(GameData data) : base(data) { }
        
        public override void Initialize() {
            _components = World.GetComponents<WeaponComponent, MoveComponent>();
            _targets = World.GetComponents<HealthComponent>();

            EventSystem<ComponentsUpdatedEvent>.Subscribe(coEvent => {
                _components = World.GetComponents<WeaponComponent, MoveComponent>();
                _targets = World.GetComponents<HealthComponent>();
            });
        }

        public override void Update() {
            foreach (var componentTuple in _components) {
                var isMoving = componentTuple.moveComponent.CurrentSpeedMove > 0;
                if (!isMoving) {
                    foreach (var target in _targets) {
                        if (componentTuple.moveComponent.Entity != target.Entity) {
                            if (Vector3.Distance(componentTuple.moveComponent.Entity.transform.position, target.Entity.transform.position) <= componentTuple.weaponComponent.Range) {

                                if (!componentTuple.weaponComponent.IsAttack) {
                                    componentTuple.weaponComponent.IsAttack = true;
                                    componentTuple.weaponComponent.Entity.Animator.SetBool(_attackAnimationKey,
                                        componentTuple.weaponComponent.IsAttack);
                                }

                                break;
                            }

                            if (!componentTuple.weaponComponent.IsAttack) {
                                continue;
                            }

                            componentTuple.weaponComponent.IsAttack = false;
                            componentTuple.weaponComponent.Entity.Animator.SetBool(_attackAnimationKey,  componentTuple.weaponComponent.IsAttack);
                        }
                    }
                } else if (componentTuple.weaponComponent.IsAttack) {
                    componentTuple.weaponComponent.IsAttack = false;
                    componentTuple.weaponComponent.Entity.Animator.SetBool(_attackAnimationKey,  componentTuple.weaponComponent.IsAttack);
                }
            }
        }
        
        private void SpherecastExample(Vector3 castPostion)
        {
            // Perform a single sphere cast using SpherecastCommand and wait for it to complete
            // Set up the command and result buffers
            var results = new NativeArray<RaycastHit>(1, Allocator.Temp);
            var commands = new NativeArray<SpherecastCommand>(1, Allocator.Temp);

            // Set the data of the first command
           // Vector3 origin = Vector3.forward * -10;
            Vector3 direction = Vector3.forward;
            float radius = 0.5f;

            commands[0] = new SpherecastCommand(castPostion, radius, direction);

            // Schedule the batch of sphere casts
            var handle = SpherecastCommand.ScheduleBatch(commands, results, 1, default(JobHandle));

            // Wait for the batch processing job to complete
            handle.Complete();

            // Copy the result. If batchedHit.collider is null, there was no hit
            foreach (var result in results) {
                if (result.collider != null) {
                    Debug.Log(result.collider.gameObject.name);
                }
            }

            // Dispose the buffers
            results.Dispose();
            commands.Dispose();
        }
    }
}