using Avocado.Core.Factories;
using Avocado.Core.Factories.Components;
using Avocado.Models.Components;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Health)]
    public class HealthComponentView : BaseComponentView<HealthComponent> {
        private readonly int _deadAnimationKey = Animator.StringToHash("Die");
        public HealthComponentView(HealthComponent componentModel, EntityView entityView) :
            base(componentModel, entityView) {
            /*Model.OnDead.AddListener(health => {
                EntityView.Animator.SetTrigger(_deadAnimationKey);
            });*/
        }
    }
}