using Avocado.Core.Factories;
using Avocado.Core.Factories.Components;
using Avocado.Models.Components;
using JetBrains.Annotations;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Inventory)]
    public class InventoryView : BaseComponentView<Inventory> {
        public InventoryView(Inventory componentModel, EntityView entityView) : base(componentModel, entityView) {
            
        }
    }
}