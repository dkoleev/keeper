using Avocado.Framework.Patterns.StateMachine;
using UnityEngine.AI;

namespace Avocado.Models.Components.AI.States {
    public class Idle : IState {
        private NavMeshAgent _agent;

        public Idle(NavMeshAgent agent) {
            _agent = agent;
        }

        public void Tick() {
        }

        public void Enter() {
            _agent.isStopped = true;
        }

        public void Exit() {
        }
    }
}