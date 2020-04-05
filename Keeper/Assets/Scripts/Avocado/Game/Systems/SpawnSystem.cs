using Avocado.Game.Data;
using Avocado.Game.Entities;
using UnityEngine;

namespace Avocado.Game.Systems {
     public class SpawnSystem : BaseSystem {
         public SpawnSystem(GameData data) : base(data) { }
         public override void Initialize() {
             var entityData = Data.Entities.Entities["Zombie"];
             var playerPrefab = Resources.Load<GameObject>(entityData.Prefab);
             var go = Object.Instantiate(playerPrefab, new Vector3(10, 0, 10), Quaternion.identity);
             var player = go.AddComponent<Entity>();
             player.Create(entityData, Data);
         }
 
         public override void Update() {
             
         }
     }
 }