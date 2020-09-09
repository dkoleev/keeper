namespace Avocado.Game.Events {
    public readonly struct PlayerDeadEvent
    {
        public readonly float LiveTime;

        public PlayerDeadEvent(float liveTime) {
            LiveTime = liveTime;
        }
    }
}