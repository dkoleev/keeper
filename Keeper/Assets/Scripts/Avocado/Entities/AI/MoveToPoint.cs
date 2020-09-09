using Avocado.Framework.Patterns.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Avocado.Game.Entities.AI {
    public class MoveToPoint : IState {
        private Animator _animator;
        private NavMeshAgent _agent;
        private static readonly int Walk = Animator.StringToHash("Walk");

        public MoveToPoint(NavMeshAgent agent, Animator animator) {
    
            _animator = animator;
            _agent = agent;
        }
        
        public void Enter() {
            var targetPoint = new Vector3(
                Random.Range(-10, 10),
                0,
                Random.Range(-10, 10));
            
            _agent.isStopped = false;
            _agent.destination = targetPoint;
            
            _animator.SetTrigger(Walk);
        }

        public void Tick() {
        }

        private void Move(Vector3 point) {
            _agent.destination = point;
        }

        public void Exit() {
            _animator.ResetTrigger(Walk);
        }
    }
}