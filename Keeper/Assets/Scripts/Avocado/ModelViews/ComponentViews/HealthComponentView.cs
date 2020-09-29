using Avocado.Data;
using Avocado.Game.Data;
using Avocado.Models.Components;
using JetBrains.Annotations;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ComponentType(ComponentType.Health)]
    public class HealthComponentView : BaseComponentView<HealthComponent> {
        public HealthComponentView(HealthComponent componentModel, EntityView entityView) :
            base(componentModel, entityView) {
        }
    }
}