using UnityEngine;
using UnityEngine.AI;

namespace Avocado.Game.Entities.AiStateMachine {
    public class IdleState : State {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private float _changeStateDelay = Random.Range(2, 5);

        public IdleState(string name, StateMachine stateMachine, NavMeshAgent agent) : base(name, stateMachine, agent) {
            
        }

        public override void Enter() {
            Agent.isStopped = true;
        }

        public override void Update() {
            if (_changeStateDelay <= 0) {
                _changeStateDelay = Random.Range(2, 4);
                StateMachine.SetWalk();
            }
            
            _changeStateDelay -= Time.deltaTime;
        }

        public override void Leave() {
        }

        public override void WalkTo(Vector3 target) {
            
        }
    }
}