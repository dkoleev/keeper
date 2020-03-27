using Avocado.Framework.Patterns.EventSystem;
using Avocado.Game.Data;
using Avocado.Game.Entities;
using Avocado.Game.Events;
using Cinemachine;
using UnityEngine;

namespace Avocado.Game.Systems {
    public class PlayerSystem : BaseSystem {
        private CinemachineBrain _brain;
        public PlayerSystem(GameData data) : base(data) { }
        
        public override void Initialize() {
            CreateActors();
        }

        public override void Update()
        {
            
        }

        private void CreateActors() {
            var entityData = Data.Entities.Entities["Player"];
            var playerPrefab = Resources.Load<GameObject>(entityData.Prefab);
            var go = Object.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            var player = go.AddComponent<Entity>();
            player.Create(entityData, Data);
            
            _brain = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineBrain>();
            if (_brain.ActiveVirtualCamera == null) {
                _brain.m_CameraActivatedEvent.AddListener((arg0, camera) => {
                    _brain.ActiveVirtualCamera.Follow = player.transform;
                });
            } else {
                _brain.ActiveVirtualCamera.Follow = player.transform;
            }
        }

        public void Dead() {
            EventSystem<PlayerDeadEvent>.Fire(new PlayerDeadEvent(10));
        }
    }
}