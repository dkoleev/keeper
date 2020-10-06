using Avocado.Framework.Patterns.Factory;
using Avocado.Core.Factories.ObjectTypes;
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
