using System;
using Avocado.Game.Core.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Avocado.Game.Entities.States.Base {
    public class EntityStateMachine : StateMachine {
        public NavMeshAgent Agent;
        public IdleState Idle { get; }
        public WalkState Walk { get; }
        
        private EntityState _currentState;
        private Entity _entity;

        public EntityStateMachine(Entity entity) {
            _entity = entity;
            Idle = new IdleState(this);
            Walk = new WalkState(this);

            _currentState = Idle;
        }

        public void ToIdle() {
            _currentState.Idle();
        }

        public void Move(Vector3 targetPoint, Action onReached) {
            _currentState.Move(targetPoint, onReached);
        }
    }
}