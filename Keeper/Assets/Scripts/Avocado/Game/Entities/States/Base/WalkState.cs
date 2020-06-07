namespace Avocado.Game.Entities.States.Base {
    public class WalkState : EntityState {
        public WalkState(EntityStateMachine stateMachine) : base(stateMachine) { }

        public override void Update() {
            
        }

        public override void Idle() {
            StateMachine.SetState(new IdleState(StateMachine));
        }
        
        
    }
}
