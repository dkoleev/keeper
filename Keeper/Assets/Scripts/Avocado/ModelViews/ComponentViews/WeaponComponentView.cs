using Avocado.Core.Factories;
using Avocado.Core.Factories.Components;
using Avocado.Models.Components;
using JetBrains.Annotations;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Weapon)]
    public class WeaponComponentView : BaseComponentView<WeaponComponent> {
        public WeaponComponentView(WeaponComponent componentModel, EntityView entityView) : base(componentModel, entityView) { }
    }
}