using Avocado.Models.Entities;
using UnityEngine;

namespace Avocado.Models.Worlds {
    public class WorldGenerator : IWorldGenerator {
        private World _world;
        private const string ZombieId = "Zombie";

        public void Generate(World world) {
            _world = world;
            GenerateEnemies();            
        }

        private void GenerateEnemies() {
            var amount = Random.Range(3, 5);
            for (int i = 0; i < amount; i++) {
                SpawnEnemy();
            }

            SpawnEnemy();
        }

        private void SpawnEnemy() {
           var entity = _world.CreateEntity<Entity>(ZombieId);
           entity.SetPosition(new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30)));
        }
    }
}