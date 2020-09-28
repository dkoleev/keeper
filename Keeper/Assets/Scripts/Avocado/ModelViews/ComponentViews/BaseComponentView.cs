using Avocado.Models.Components;

namespace Avocado.ModelViews.ComponentViews {
    public class BaseComponentView {
        protected EntityView EntityView;
        protected IComponent Model;
        public BaseComponentView(IComponent componentModel, EntityView entityView) {
            Model = componentModel;
            EntityView = entityView;
        }

        public virtual void Initialize() { }

        public virtual void Update() { }
    }
}
