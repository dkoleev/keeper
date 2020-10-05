using Avocado.Core.Factories.Components;
using Avocado.Data;
using Avocado.Game.Data;
using Avocado.Models.Components;
using JetBrains.Annotations;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ComponentType(ComponentType.Inventory)]
    public class InventoryView : BaseComponentView<Inventory> {
        public InventoryView(Inventory componentModel, EntityView entityView) : base(componentModel, entityView) {
            
        }
    }
}