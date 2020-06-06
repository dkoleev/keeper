using Avocado.Game.Data;
using Cinemachine;
using UnityEngine;

namespace Avocado.Game.Entities {
    public class PlayerEntity : Entity {
        private CinemachineBrain _brain;

        public override void Initialize(string entityId, in EntityData entityData, in GameData gameData) {
            base.Initialize(entityId, in entityData, in gameData);
            
            _brain = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineBrain>();
            if (_brain.ActiveVirtualCamera == null) {
                _brain.m_CameraActivatedEvent.AddListener((arg0, camera) => {
                    _brain.ActiveVirtualCamera.Follow = transform;
                });
            } else {
                _brain.ActiveVirtualCamera.Follow = transform;
            }
        }
    }
}