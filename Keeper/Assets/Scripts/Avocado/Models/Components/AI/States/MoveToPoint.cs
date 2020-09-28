using Avocado.Framework.Patterns.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Avocado.Models.Components.AI.States {
    public class MoveToPoint : IState {
        private NavMeshAgent _agent;

        public MoveToPoint(NavMeshAgent agent) {
            _agent = agent;
        }
        
        public void Enter() {
            var targetPoint = new Vector3(
                Random.Range(-10, 10),
                0,
                Random.Range(-10, 10));
            
            _agent.isStopped = false;
            _agent.destination = targetPoint;
        }

        public void Tick() {
        }

        private void Move(Vector3 point) {
            _agent.destination = point;
        }

        public void Exit() {
        }
    }
}