using Avocado.Core.Factories.Components;
using Avocado.Data;
using Avocado.Game.Data;
using Avocado.Models.Components;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ComponentType(ComponentType.Health)]
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