using System.Linq;
using Avocado.ModelViews.ComponentViews;
using UnityEngine;
using UnityEngine.UI;

namespace Avocado.UI.World {
    public class WorldBar : WorldUiElement {
        [SerializeField] private Image _progress;

        private HealthComponentView _health;

        protected override void Initialize() {
            base.Initialize();
            
            _health = (HealthComponentView)EntityView.Components.FirstOrDefault(view => view is HealthComponentView);
            
            SetProgress((float)_health.Model.CurrentHealth / _health.Model.MaxHealth);
            _health.Model.OnHealthChange.AddListener((prev, current, max) => {
                var newValue = (float)current / max;
                SetProgress(newValue);
            });
            _health.Model.OnDead.AddListener(component => {
                gameObject.SetActive(false);
            });
        }

        private void SetProgress(float value) {
            _progress.fillAmount = Mathf.Clamp01(value);
        }
    }
}