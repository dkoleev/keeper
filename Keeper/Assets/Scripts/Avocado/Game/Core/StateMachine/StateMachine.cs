namespace Avocado.Game.Core.StateMachine {
    public abstract class StateMachine {
        protected IState CurrentState;

        public void SetState(IState state) {
            CurrentState = state;
        }

        public virtual void Update() {
            CurrentState.Update();
        }
    }
}
