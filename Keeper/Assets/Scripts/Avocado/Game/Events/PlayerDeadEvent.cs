using Avocado.Framework.Patterns.EventSystem;

namespace Avocado.Game.Events {
    public class PlayerDeadEvent : IEvent {
        public float LiveTime { get;} 

        public PlayerDeadEvent(float liveTime) {
            LiveTime = liveTime;
        }
    }
}