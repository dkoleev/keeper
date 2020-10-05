using Avocado.Core.Factories.Components;
using Avocado.Data;
using Avocado.Framework.Patterns.StateMachine;
using Avocado.Game.Data;
using Avocado.Models.Components.AI;
using Avocado.Models.Components.AI.States;
using Avocado.ModelViews.ComponentViews.AI.States;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Avocado.ModelViews.ComponentViews.AI {
    [UsedImplicitly]
    [ComponentType(ComponentType.AI)]
    public class BaseAiView : BaseComponentView<BaseAi> {
        private Animator _animator;
        private NavMeshAgent _agent;
        
        private static readonly int IdleState = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private readonly int _deadAnimationKey = Animator.StringToHash("Die");

        public BaseAiView(BaseAi componentModel, EntityView entityView) : base(componentModel, entityView) {
            _agent = EntityView.GetComponent<NavMeshAgent>();
            Model.SetNavMeEshAgent(_agent);
            if (!Model.IsAlive) {
                EntityView.Animator.SetTrigger(_deadAnimationKey);
            }
            Model.OnStateChanged.AddListener(ModelStateChanged);
     
        }

        private void ModelStateChanged(IState prevState, IState newState) {
            if (prevState is IdleState) {
                EntityView.Animator.ResetTrigger(IdleState);
            }
            
            if (prevState is MoveToPoint) {
                EntityView.Animator.ResetTrigger(Walk);
            }
            
            if (newState is IdleState) {
                EntityView.Animator.SetTrigger(IdleState);
            }

            if (newState is MoveToPoint) {
                EntityView.Animator.SetTrigger(Walk);
            }
            
            if (newState is Die) {
                EntityView.Animator.SetTrigger(_deadAnimationKey);
            }
        }
    }
}