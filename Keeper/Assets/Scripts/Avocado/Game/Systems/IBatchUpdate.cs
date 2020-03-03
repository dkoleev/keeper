namespace Avocado.Game.Systems {
    public interface IBatchUpdate {
        void Register();
        void BatchUpdate();
    }
}