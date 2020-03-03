using Avocado.Game.Data;
using Avocado.Game.Entities;
using Cinemachine;
using UnityEngine;

namespace Avocado.Game.Systems {
    public class PlayerSystem : BaseSystem, IBatchUpdate {
        private CinemachineBrain _brain;
        public PlayerSystem(GameData data) : base(data) { }
        
        public override void Initialize() {
            CreateActors();
            Register();
        }

        private void CreateActors() {
            var playerPrefab = Resources.Load<GameObject>(Data.Player.Entity.Prefab);
            var go = Object.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            var player = go.AddComponent<Player>();
            player.Initialize(Data);
            _brain = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineBrain>();
            if (_brain.ActiveVirtualCamera == null) {
                _brain.m_CameraActivatedEvent.AddListener((arg0, camera) => {
                    _brain.ActiveVirtualCamera.Follow = player.transform;
                });
            } else {
                _brain.ActiveVirtualCamera.Follow = player.transform;
            }
        }

        public void Register() {
            UpdateSystem.Instance.RegisterSlicedUpdate(this, UpdateSystem.UpdateMode.Always);
        }

        public void BatchUpdate() {

        }
    }
}