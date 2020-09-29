using Avocado.Game.Data;
using Avocado.Models.Components;
using JetBrains.Annotations;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ComponentType(ComponentType.None)]
    public class BaseComponentView : IComponentView {
        public EntityView EntityView { get; }
        public IComponent Model { get; }
        
        public BaseComponentView(IComponent componentModel, EntityView entityView) {
            Model = componentModel;
            EntityView = entityView;
        }

        public virtual void Initialize() { }

        public virtual void Update() { }
    }
}
