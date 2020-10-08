using Avocado.Core.SaveEngine;

namespace Avocado.Progress {
    public class GameProgress : IProgress {
        public PlayerProgress Player = new PlayerProgress();
    }
}