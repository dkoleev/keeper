namespace Avocado.Game.Core.StateMachine {
    public interface IState {
        void Tick();
        void Enter();
        void Exit();
    }
}
