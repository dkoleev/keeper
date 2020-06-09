using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Avocado.Game.Entities.AiStateMachine {
    public class StateMachine {
        private State _currentState;
        private State _idleState;
        private State _walkState;
        
        private int _statesAmount;
        private Action _onTargetReached;
        private NavMeshAgent _agent;
        private Animator _animator;

        public StateMachine(NavMeshAgent agent, Animator animator) {
            _agent = agent;
            _animator = animator;
            
            _idleState = new IdleState(this, _agent, _animator);
            _walkState = new WalkState(this, _agent, _animator);
        }

        public void Update() {
            _currentState.Update();
        }

        public void SetIdle() {
            SetState(_idleState);
        }

        public void SetWalk() {
           SetState(_walkState);
           _currentState.WalkTo(new Vector3(
               Random.Range(-10, 10),
               0,
               Random.Range(-10, 10)));
        }
        
        private void SetState(State state) {
            _currentState?.Leave();
            _currentState = state;
            _currentState.Enter();
        }
    }
}