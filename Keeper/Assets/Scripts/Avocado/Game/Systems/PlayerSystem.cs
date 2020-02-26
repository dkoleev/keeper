using Avocado.Game.Data;
using Avocado.Game.Entities;
using UnityEngine;

namespace Avocado.Game.Systems {
    public class PlayerSystem : BaseSystem {
        public PlayerSystem(GameData data) : base(data) {
        }

        protected override void CreateActors() {
            var playerPrefab = Resources.Load<GameObject>(Data.Player.Entity.Prefab);
            var go = Object.Instantiate(playerPrefab);
            var player = go.AddComponent<Player>();
            player.Initialize(Data);
        }
    }
}