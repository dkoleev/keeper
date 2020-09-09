namespace Avocado.Game.Core {
    public interface IInitializable {
        bool Initialized { get; }
        void Initialize();
    }
}
