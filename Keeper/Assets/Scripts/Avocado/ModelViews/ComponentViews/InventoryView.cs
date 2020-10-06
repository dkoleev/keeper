using Avocado.Framework.Patterns.Factory;
using Avocado.Core.Factories.ObjectTypes;
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