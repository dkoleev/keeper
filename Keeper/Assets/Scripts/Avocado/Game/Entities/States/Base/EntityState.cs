using System;
using Avocado.Game.Core.StateMachine;
using UnityEngine;

namespace Avocado.Game.Entities.States.Base {
    public abstract class EntityState : IState {
        protected EntityStateMachine StateMachine;

        protected EntityState(EntityStateMachine stateMachine) {
            StateMachine = stateMachine;
        }
        
        public virtual void Update() {
            
        }

        public virtual void Move(Vector3 targetPoint, Action onTargetReached) {
            
        }
        
        public virtual void Idle() { }
    }
}