using Avocado.Models.Components;

namespace Avocado.ModelViews.ComponentViews {
    public interface IComponentView {
        EntityView EntityView { get; }
        IComponent Model { get; }
        void Initialize();
        void Update();
    }
}