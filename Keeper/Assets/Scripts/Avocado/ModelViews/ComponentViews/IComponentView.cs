namespace Avocado.ModelViews.ComponentViews {
    public interface IComponentView {
        EntityView EntityView { get; }
        void Initialize();
        void Update();
    }
}