using System.Collections.Generic;
using Avocado.Models.Components;
using Avocado.Models.Components.AI;
using Avocado.ModelViews.ComponentViews.AI;

namespace Avocado.ModelViews.ComponentViews {
    public class ComponentsViewFactory : IComponentViewFactory {
        public BaseComponentView Create(IComponent component, EntityView entityView) {
            if (component is BaseAi baseAi) {
                return new BaseAiView(baseAi, entityView);
            }
            if (component is AttackComponent attackComponent) {
                return new AttackComponentView(attackComponent, entityView);
            }
            if (component is ControlsComponent controlsComponent) {
                return new ControlsComponentView(controlsComponent, entityView);
            }

            return new BaseComponentView(component, entityView);
        }

        public List<BaseComponentView> Create(IEnumerable<IComponent> components, EntityView entityView) {
            var result = new List<BaseComponentView>();
            foreach (var component in components) {
                result.Add(Create(component, entityView));
            }

            return result;
        }
    }
}
