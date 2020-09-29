using Avocado.Models.Entities;
using Avocado.ModelViews.ComponentViews;
using Cinemachine;
using UnityEngine;

namespace Avocado.ModelViews {
    public class PlayerEntityView : EntityView {
        private CinemachineBrain _brain;

        public override void Initialize(Entity entity, WorldView worldView) {
            base.Initialize(entity, worldView);
            
            _brain = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineBrain>();
            if (_brain.ActiveVirtualCamera == null) {
                _brain.m_CameraActivatedEvent.AddListener((arg0, cam) => {
                    _brain.ActiveVirtualCamera.Follow = transform;
                });
            } else {
                _brain.ActiveVirtualCamera.Follow = transform;
            }
        }
    }
}
