using System;
using Avocado.Game.Core.StateMachine;
using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using Avocado.Game.Entities.AI;
using Avocado.Game.Systems;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Logger = Avocado.Framework.Utilities.Logger;
using Random = UnityEngine.Random;
using StateMachine = Avocado.Game.Core.StateMachine.StateMachine;

namespace Avocado.Game.Components.AI {
    [UsedImplicitly]
    [ComponentType(ComponentType.AI)]
    public class BaseAi : ComponentBase<AiComponentData> {
        private NavMeshAgent _agent;
        private Animator _animator;
        private MoveComponent _moveComponent;
       //private StateMachine _stateMachine;
        private AnimationSystem _animationSystem;
        private StateMachine _stateMachine;
        private float _idleDelay = Random.Range(2, 5);
        private Action _canMove;
        
        public BaseAi(Entity entity, AiComponentData data) : base(entity, data) {
            _agent = Entity.GetComponent<NavMeshAgent>();
            if (_agent is null) {
                Logger.LogError($"Not found NavMeshAgent component for entity {Entity.EntityId}");
            }

            _animator = Entity.GetComponentInChildren<Animator>();
            _animationSystem = new AnimationSystem(_animator);
            _stateMachine = new StateMachine();
            
            var idle = new Idle(_agent, _animator);
            var walkState = new MoveToPoint(_agent, _animator);
            
            _stateMachine.AddAnyTransition(walkState, CanMove());
            At(walkState, idle, IsTargetReached);
            
            
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            
            Func<bool> CanMove() => () => _idleDelay <= 0f;
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
        
        public override void Initialize() {
            _moveComponent = (MoveComponent)Entity.GetComponentByType<MoveComponent>();
            _agent.speed = _moveComponent.SpeedMove;
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