using Avocado.Framework.Patterns.EventSystem;

namespace Avocado.Game.Events {
    public struct PlayerDeadEvent {
        public float LiveTime { get;} 

        public PlayerDeadEvent(float liveTime) {
            LiveTime = liveTime;
        }
    }
}