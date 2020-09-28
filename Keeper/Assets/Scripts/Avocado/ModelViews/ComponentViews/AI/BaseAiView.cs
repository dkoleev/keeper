using Avocado.Framework.Patterns.StateMachine;
using Avocado.Models.Components.AI;
using Avocado.Models.Components.AI.States;
using Avocado.ModelViews.ComponentViews.AI.States;
using UnityEngine;
using UnityEngine.AI;

namespace Avocado.ModelViews.ComponentViews.AI {
    public class BaseAiView : BaseComponentView {
        private Animator _animator;
        private BaseAi AiModel;
        private NavMeshAgent _agent;
        
        private static readonly int IdleState = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Walk");

        public BaseAiView(BaseAi componentModel, EntityView entityView) : base(componentModel, entityView) {
            _agent = EntityView.GetComponent<NavMeshAgent>();
            AiModel = componentModel;
            AiModel.SetNavMeEshAgent(_agent);
            AiModel.OnStateChanged.AddListener(ModelStateChanged);
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
        }
    }
}