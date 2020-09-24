using Avocado.Framework.Patterns.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Avocado.Models.Entities.AI {
    public class Idle : IState {
        private Animator _animator;
        private NavMeshAgent _agent;
        private static readonly int IdleState = Animator.StringToHash("Idle");

        public Idle(NavMeshAgent agent, Animator animator) {
            _animator = animator;
            _agent = agent;
        }

        public void Tick() {
            
        }

        public void Enter() {
            _agent.isStopped = false;
            _animator.SetTrigger(IdleState);
        }

        public void Exit() {
            _animator.ResetTrigger(IdleState);
        }
    }
}