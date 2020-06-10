using System;
using Avocado.Game.Systems;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Avocado.Game.Entities.AiStateMachine {
    public class StateMachine {
        public Action<State> OnStateChanged;
        private State _currentState;
        private State _idleState;
        private State _walkState;
        
        private int _statesAmount;
        private Action _onTargetReached;
        private NavMeshAgent _agent;

        public StateMachine(NavMeshAgent agent, Animator animator) {
            _agent = agent;
            
            _idleState = new IdleState("Idle",this, _agent);
            _walkState = new WalkState("Walk",this, _agent);
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
            
            OnStateChanged?.Invoke(_currentState);
        }
    }
}