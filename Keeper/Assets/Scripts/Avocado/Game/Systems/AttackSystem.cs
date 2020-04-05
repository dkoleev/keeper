using System.Collections.Generic;
using Avocado.Framework.Patterns.EventSystem;
using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Events;
using Avocado.Game.Worlds;
using UnityEngine;

namespace Avocado.Game.Systems {
    public class AttackSystem : BaseSystem {
        private readonly int _attackAnimationKey = Animator.StringToHash("Attack");
        private List<(WeaponComponent weaponComponent, MoveComponent moveComponent)> _components = new List<(WeaponComponent, MoveComponent)>();

        public AttackSystem(GameData data) : base(data) { }
        
        public override void Initialize() {
            _components = World.GetComponents<WeaponComponent, MoveComponent>();
            EventSystem<ComponentsUpdatedEvent>.Subscribe(coEvent =>  _components = World.GetComponents<WeaponComponent, MoveComponent>());
        }

        public override void Update() {
            foreach (var componentTuple in _components) {
                var canAttack = componentTuple.moveComponent.CurrentSpeedMove <= 0;
                if (componentTuple.weaponComponent.IsAttack != canAttack) {
                    componentTuple.weaponComponent.IsAttack = canAttack;
                    componentTuple.weaponComponent.Entity.Animator.SetBool(_attackAnimationKey, canAttack);
                }
            }
        }
    }
}