using Avocado.Game.Entities;
using UnityEngine;

namespace Avocado.Game.Worlds {
    public class WorldGenerator {
        public void Generate() {
            GenerateEnemies();            
        }

        private void GenerateEnemies() {
            var amount = Random.Range(3, 5);
            for (int i = 0; i < amount; i++) {
                SpawnEnemy();
            }

            void SpawnEnemy() {
                World.CreateEntity<Entity>("Zombie",
                    new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30)), null, entity => {
                        
                    });
            }
        }
    }
}