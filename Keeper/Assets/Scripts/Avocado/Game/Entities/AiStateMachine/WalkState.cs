using UnityEngine;
using UnityEngine.AI;

namespace Avocado.Game.Entities.AiStateMachine {
    public class WalkState : State {
        private static readonly int Walk = Animator.StringToHash("Walk");

        public WalkState(string name, StateMachine stateMachine, NavMeshAgent agent) : base(name, stateMachine, agent) {
            
        }

        public override void Enter() {
        }

        public override void Update() {
            if (IsTargetReached()) {
                StateMachine.SetIdle();
            }
        }

        public override void WalkTo(Vector3 target) {
            Agent.isStopped = false;
            MoveToPoint(target);
        }

        private bool IsTargetReached() {
            if (!Agent.pathPending) {
                if (Agent.remainingDistance <= Agent.stoppingDistance) {
                    if (!Agent.hasPath || Agent.velocity.sqrMagnitude <= 0f) {
                        return true;
                    }
                }
            }

            return false;
        }
        
        private void MoveToPoint(Vector3 point) {
            Agent.destination = point;
        }
        
        public override void Leave() {
        }
    }
}