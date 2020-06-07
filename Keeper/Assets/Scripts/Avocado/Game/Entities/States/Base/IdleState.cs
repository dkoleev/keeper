using System;
using UnityEngine;

namespace Avocado.Game.Entities.States.Base {
    public class IdleState : EntityState {
        public IdleState(EntityStateMachine stateMachine) : base(stateMachine) { }

        public override void Move(Vector3 targetPoint, Action onTargetReached) {
            StateMachine.SetState(new WalkState(StateMachine));
        }
    }
}
