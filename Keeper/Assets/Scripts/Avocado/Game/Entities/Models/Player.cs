using System;

namespace Avocado.Game.Entities.Models {
    public class Player : Entity {
        public enum PlayerState {
            Idle,
            Move
        }

        public Action<PlayerState, PlayerState> OnStateChanged;

        public PlayerState CurrentState => _currentState;
        private PlayerState _currentState = PlayerState.Idle;

        public void SetState(PlayerState state) {
            if (_currentState != state) {
                OnStateChanged?.Invoke(_currentState, state);
                _currentState = state;
            }
        }
    }
}