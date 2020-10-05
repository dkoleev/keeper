using Avocado.Core.Factories;
using Avocado.Core.Factories.Components;
using Avocado.Models.Components;
using JetBrains.Annotations;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Move)]
    public class MoveComponentView : BaseComponentView<MoveComponent> {
        public MoveComponentView(MoveComponent componentModel, EntityView entityView) :
            base(componentModel, entityView) {
            
        }
    }
}
