using Avocado.Core.Factories.Components;
using Avocado.Data;
using Avocado.Game.Data;
using Avocado.Models.Components;
using JetBrains.Annotations;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ComponentType(ComponentType.Weapon)]
    public class WeaponComponentView : BaseComponentView<WeaponComponent> {
        public WeaponComponentView(WeaponComponent componentModel, EntityView entityView) : base(componentModel, entityView) { }
    }
}