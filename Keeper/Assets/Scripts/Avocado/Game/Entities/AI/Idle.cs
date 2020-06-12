using Avocado.Framework.Patterns.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Avocado.Game.Entities.AI {
    public class Idle : IState {
        private Animator _animator;
        private NavMeshAgent _agent;

        public Idle(NavMeshAgent agent, Animator animator) {
            _animator = animator;
            _agent = agent;
        }

        public void Tick() {
            
        }

        public void Enter() {
            _agent.isStopped = false;
            _animator.SetTrigger("Idle");
        }

        public void Exit() {
            _animator.ResetTrigger("Idle");
        }
    }
}