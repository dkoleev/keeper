using Avocado.Game.Data;
using Avocado.Game.Entities;
using UnityEngine;

namespace Avocado.Game.Systems {
     public class SpawnSystem : BaseSystem {
         public SpawnSystem(GameData data) : base(data) { }
         public override void Initialize() {
             Entity.Create(Data, "Zombie", new Vector3(10, 0, 10));
         }
 
         public override void Update() {
             
         }
     }
 }