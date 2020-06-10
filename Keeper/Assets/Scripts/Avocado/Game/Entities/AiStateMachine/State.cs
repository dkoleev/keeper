using UnityEngine;
using UnityEngine.AI;

namespace Avocado.Game.Entities.AiStateMachine {
    public abstract class State {
        public readonly string Name;
        protected readonly StateMachine StateMachine;
        protected readonly NavMeshAgent Agent;

        protected State(string name, StateMachine stateMachine, NavMeshAgent agent) {
            Name = name;
            StateMachine = stateMachine;
            Agent = agent;
        }

        public abstract void Enter();

        public abstract void Update();
        public abstract void Leave();
        public abstract void WalkTo(Vector3 target);
    }
}