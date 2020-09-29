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

        public BaseAiView(BaseAi componentModel, EntityView entityView) : base(componentModel, entityView) {
            _agent = EntityView.GetComponent<NavMeshAgent>();
            Model.SetNavMeEshAgent(_agent);
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
        }
    }
}