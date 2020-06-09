using UnityEngine;
using UnityEngine.AI;

namespace Avocado.Game.Entities.AiStateMachine {
    public abstract class State {
        protected readonly StateMachine StateMachine;
        protected readonly NavMeshAgent Agent;
        protected readonly Animator Animator;

        protected State(StateMachine stateMachine, NavMeshAgent agent, Animator animator) {
            StateMachine = stateMachine;
            Agent = agent;
            Animator = animator;
        }

        public abstract void Enter();

        public abstract void Update();
        public abstract void Leave();
        public abstract void WalkTo(Vector3 target);
    }
}