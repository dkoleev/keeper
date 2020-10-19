using Avocado.Framework.Patterns.Factory;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Framework.Patterns.StateMachine;
using Avocado.Models.Components.AI;
using Avocado.Models.Components.AI.States;
using Avocado.UnityToolbox.Timer;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Avocado.ModelViews.ComponentViews.AI {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.AI)]
    public class BaseAiView : BaseComponentView<BaseAi> {
        private Animator _animator;
        private NavMeshAgent _agent;
        
        private static readonly int IdleState = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private readonly int _deadAnimationKey = Animator.StringToHash("Die");

        public BaseAiView(BaseAi componentModel, EntityView entityView) : base(componentModel, entityView) {
            _agent = EntityView.GetComponentInChildren<NavMeshAgent>();
            Model.SetNavMeEshAgent(_agent);
            if (!Model.IsAlive) {
                EntityView.Animator.SetTrigger(_deadAnimationKey);
            }
            Model.OnStateChanged.AddListener(ModelStateChanged);
     
        }

        private void ModelStateChanged(IState prevState, IState newState) {
            EntityView.Animator.ResetTrigger(Walk);
            EntityView.Animator.ResetTrigger(IdleState);
            EntityView.Animator.ResetTrigger(_deadAnimationKey);
            
            if (newState is Idle) {
                EntityView.Animator.SetTrigger(IdleState);
            }

            if (newState is MoveToPoint) {
                EntityView.Animator.SetTrigger(Walk);
            }
            
            if (newState is Die) {
                EntityView.Animator.SetTrigger(_deadAnimationKey);
                var timer = new TimeManager();
                timer.Call(2.0f, () => {
                    EntityView.MoveTransform.DOScale(Vector3.zero, 1);
                    timer.Call(1.0f, () => {
                        EntityView.MoveTransform.gameObject.SetActive(false);
                    });
                });
            }
        }
    }
}