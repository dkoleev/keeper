using Avocado.Framework.Patterns.Singleton;

namespace Avocado.Core.Controls {
    public class ControlsManager : Singleton<ControlsManager> {
        public Controls MainControl { get; }

        public ControlsManager() {
            MainControl = new Controls();
        }
    }
}