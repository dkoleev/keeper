using System;
using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Logger = Avocado.Framework.Utilities.Logger;
using Random = UnityEngine.Random;

namespace Avocado.Game.Components.AI {
    [UsedImplicitly]
    [ComponentType(ComponentType.AI)]
    public class BaseAi : ComponentBase<AiComponentData> {
        private enum State {
            Idle,
            Walk
        }

        private NavMeshAgent _agent;
        private Animator _animator;
        private MoveComponent _moveComponent;
        private State _currentState;
        private float _changeStateDelay = Random.Range(2, 5);
        private int _statesAmount;
        private Action _onTargetReached;
        
        public BaseAi(Entity entity, AiComponentData data) : base(entity, data) {
            _agent = Entity.GetComponent<NavMeshAgent>();
            if (_agent is null) {
                Logger.LogError($"Not found NavMeshAgent component for entity {Entity.EntityId}");
            }

            _statesAmount = Enum.GetNames(typeof(State)).Length;
            _animator = Entity.GetComponentInChildren<Animator>();

            SetState(State.Idle);
        }
        
        public override void Initialize() {
            _moveComponent = (MoveComponent)Entity.GetComponentByType<MoveComponent>();
            _agent.speed = _moveComponent.SpeedMove;
        }

        public override void Update() {
            switch (_currentState) {
                case State.Idle:
                    if (_changeStateDelay <= 0) {
                        _changeStateDelay = Random.Range(2, 4);
                        SetState(State.Walk);
                    }
                    _changeStateDelay -= Time.deltaTime;
                    break;
                case State.Walk:
                    if (IsTargetReached()) {
                        SetState(State.Idle);
                        _onTargetReached?.Invoke();
                    }
                    break;
            }
        }

        private void MoveToPoint(Vector3 point) {
            _agent.destination = point;
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

        private void SetState(State state) {
            switch (state) {
                case State.Walk:
                    _agent.isStopped = false;
                    MoveToPoint(new Vector3(
                        Random.Range(-10, 10),
                        0, 
                        Random.Range(-10, 10)));
                    break;
                case State.Idle:
                    _agent.isStopped = true;
                    break;
            }
            
            if (_animator != null) {
                _animator.ResetTrigger(State.Idle.ToString());
                _animator.ResetTrigger(State.Walk.ToString());
                _animator.SetTrigger(state.ToString());
            }
            
            _currentState = state;
        }
    }
}