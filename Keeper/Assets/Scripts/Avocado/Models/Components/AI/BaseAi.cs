using System;
using Avocado.Data;
using Avocado.Framework.Patterns.StateMachine;
using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Models.Components.AI.States;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using Sigtrap.Relays;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Avocado.Models.Components.AI {
    [UsedImplicitly]
    [ComponentType(ComponentType.AI)]
    public class BaseAi : ComponentBase<AiComponentData> {
        public Relay<IState, IState> OnStateChanged = new Relay<IState, IState>();
        
        private MoveComponent _moveComponent;
        private StateMachine _stateMachine;
        private float _idleDelay = Random.Range(2, 5);
        private Action _canMove;
        private NavMeshAgent _agent;

        private IState _idleState;
        private IState _moveState;

        public BaseAi(Entity entity, AiComponentData data) : base(entity, data) {
            _stateMachine = new StateMachine();

            _stateMachine.OnStateChanged += (prevState, newState) => {
                OnStateChanged.Dispatch(prevState, newState);
            };
        }

        public override void Initialize() {
            base.Initialize();
            _moveComponent = (MoveComponent)Entity.GetComponentByType<MoveComponent>();
        }

        public void SetNavMeEshAgent(NavMeshAgent agent) {
            _agent = agent;
            _agent.speed = _moveComponent.SpeedMove;
            CreateStates();
        }

        private void CreateStates() {
            _idleState = new Idle(_agent);
            _moveState = new MoveToPoint(_agent);
            
            To(_moveState, CanMove());
            At(_moveState, _idleState, IsTargetReached);

            void To(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            
            Func<bool> CanMove() => () => _idleDelay <= 0f;
            
            _stateMachine.SetState(_idleState);
        }

        private bool IsTargetReached() {
            if (!_agent.pathPending) {
                if (_agent.remainingDistance <= _agent.stoppingDistance) {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude <= 0f) {
                        return true;
                    }
                }
            }

            return false;
        }

        public override void Update() {
           _stateMachine.Tick();
           UpdateIdleTime();
        }

        private void UpdateIdleTime() {
            if (_idleDelay <= 0) {
                _idleDelay = Random.Range(2, 4);
            }
            
            _idleDelay -= Time.deltaTime;
        }
    }
}