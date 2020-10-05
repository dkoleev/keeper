using Avocado.Core.Factories.Components;
using Avocado.Data;
using Avocado.Game.Data;
using Avocado.Models.Components;
using JetBrains.Annotations;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ComponentType(ComponentType.Move)]
    public class MoveComponentView : BaseComponentView<MoveComponent> {
        public MoveComponentView(MoveComponent componentModel, EntityView entityView) :
            base(componentModel, entityView) {
            
        }
    }
}
