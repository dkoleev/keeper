using Avocado.Models.Components;

namespace Avocado.ModelViews.ComponentViews {
    public class BaseComponentView<TComponentModel> : IComponentView
        where TComponentModel : IComponent {
        public EntityView EntityView { get; }
        public TComponentModel Model { get; }
        
        public BaseComponentView(TComponentModel componentModel, EntityView entityView) {
            Model = componentModel;
            EntityView = entityView;
        }

        public virtual void Initialize() { }

        public virtual void Update() { }
    }
}
