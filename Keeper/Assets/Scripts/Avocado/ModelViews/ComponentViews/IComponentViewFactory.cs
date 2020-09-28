using System.Collections.Generic;
using Avocado.Models.Components;

namespace Avocado.ModelViews.ComponentViews {
    public interface IComponentViewFactory {
        BaseComponentView Create(IComponent component, EntityView entityView);
        List<BaseComponentView> Create(IEnumerable<IComponent> components, EntityView entityView);
    }
}
