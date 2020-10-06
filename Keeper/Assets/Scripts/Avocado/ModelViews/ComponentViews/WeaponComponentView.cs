using Avocado.Framework.Patterns.Factory;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Models.Components;
using JetBrains.Annotations;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Weapon)]
    public class WeaponComponentView : BaseComponentView<WeaponComponent> {
        public WeaponComponentView(WeaponComponent componentModel, EntityView entityView) : base(componentModel, entityView) { }
    }
}