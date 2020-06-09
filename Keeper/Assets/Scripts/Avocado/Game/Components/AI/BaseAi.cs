using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using Avocado.Game.Entities.AiStateMachine;
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
        
        public BaseAi(Entity entity, AiComponentData data) : base(entity, data) {
            _agent = Entity.GetComponent<NavMeshAgent>();
            if (_agent is null) {
                Logger.LogError($"Not found NavMeshAgent component for entity {Entity.EntityId}");
            }

            _animator = Entity.GetComponentInChildren<Animator>();
            _stateMachine = new StateMachine(_agent, _animator);
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