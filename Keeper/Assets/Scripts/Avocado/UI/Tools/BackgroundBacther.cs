using UnityEngine;
using UnityEngine.UI;

namespace Avocado.Game.UI.Tools {
    public class BackgroundBacther {
        private RawImage _touchBlockerImage;
        private RenderTexture _currentTexture;
        private LayerMask _savedCullMask;
        
        public void BatchBackground() {
            var camera = Camera.main;
            if (_currentTexture is null) {
                _currentTexture =
                    new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);

                _currentTexture.Create();
            }

            camera.targetTexture = _currentTexture;
            camera.Render();
            camera.targetTexture = null;
            _touchBlockerImage.texture = _currentTexture;
            _touchBlockerImage.gameObject.SetActive(true);
            _savedCullMask = camera.cullingMask;
            camera.cullingMask = 0;
        }

        public void UnbatchBackground() {
            _touchBlockerImage.gameObject.SetActive(false);
            var camera = Camera.main;
            camera.cullingMask = _savedCullMask;
        }
    }
}