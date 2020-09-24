using Avocado.Game.Data;
using Avocado.Models.Worlds;
using Cinemachine;
using UnityEngine;

namespace Avocado.Models.Entities {
    public class PlayerEntity : Entity {
        private CinemachineBrain _brain;

        public override void Initialize(string entityId, in EntityData entityData, World world) {
            base.Initialize(entityId, in entityData, world);
            
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