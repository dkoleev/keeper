using System.Linq;
using Avocado.ModelViews.ComponentViews;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Avocado.UI.World {
    public class WorldBar : WorldUiElement {
        [SerializeField] private Image _progress;
        [SerializeField] private Image _bufferProgress;

        private HealthComponentView _health;

        protected override void Initialize() {
            base.Initialize();
            
            _health = (HealthComponentView)EntityView.Components.FirstOrDefault(view => view is HealthComponentView);

            var value = (float) _health.Model.CurrentHealth / _health.Model.MaxHealth;
            SetProgress(value, value);
            _health.Model.OnHealthChange.AddListener((prev, current, max) => {
                var prevValue = (float)prev / max;
                var newValue = (float)current / max;
                SetProgress(newValue, prevValue);
            });
            _health.Model.OnDead.AddListener(component => {
                gameObject.SetActive(false);
            });
        }

        private void SetProgress(float value, float prevValue) {
            _progress.fillAmount = Mathf.Clamp01(value);
            _bufferProgress.DOFillAmount(value, 1.0f);
        }
    }
}