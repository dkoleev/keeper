using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using Avocado.Game.Entities.AiStateMachine;
using Avocado.Game.Systems;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Logger = Avocado.Framework.Utilities.Logger;

namespace Avocado.Game.Components.AI {
    [UsedImplicitly]
    [ComponentType(ComponentType.AI)]
    public class BaseAi : ComponentBase<AiComponentData> {
        private NavMeshAgent _agent;
        private Animator _animator;
        private MoveComponent _moveComponent;
        private StateMachine _stateMachine;
        private AnimationSystem _animationSystem;
        
        public BaseAi(Entity entity, AiComponentData data) : base(entity, data) {
            _agent = Entity.GetComponent<NavMeshAgent>();
            if (_agent is null) {
                Logger.LogError($"Not found NavMeshAgent component for entity {Entity.EntityId}");
            }

            _animator = Entity.GetComponentInChildren<Animator>();
            _animationSystem = new AnimationSystem(_animator);
            _stateMachine = new StateMachine(_agent, _animator);
            _stateMachine.OnStateChanged += state => {
                _animationSystem.SetState(state.Name);
            };
            
            _stateMachine.SetIdle();
        }
        
        public override void Initialize() {
            _moveComponent = (MoveComponent)Entity.GetComponentByType<MoveComponent>();
            _agent.speed = _moveComponent.SpeedMove;
        }

        public override void Update() {
            _stateMachine.Update();
        }
    }
}