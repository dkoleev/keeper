using Avocado.ModelViews;
using UnityEngine;

namespace Avocado.UI.World {
    public class WorldUiElement : MonoBehaviour {
        protected EntityView EntityView;
        protected bool Initialized;

        protected virtual void Initialize() {
            Initialized = true;
        }

        private void Start() {
            TryInitializeEntityView();
        }

        protected virtual void Update() {
            TryInitializeEntityView();
        }

        private void TryInitializeEntityView() {
            if (Initialized) {
                return;
            }

            if (EntityView is null) {
                EntityView = transform.root.GetComponent<EntityView>();
            }

            if (!(EntityView is null)) {
                if (EntityView.Initialized) {
                    Initialize();
                }
            }
        }
    }
}