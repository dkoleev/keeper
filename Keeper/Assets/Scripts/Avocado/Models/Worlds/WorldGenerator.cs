using System.Collections.Generic;
using Avocado.Models.Components;
using Avocado.Models.Entities;
using Avocado.UnityToolbox.Timer;
using UnityEngine;

namespace Avocado.Models.Worlds {
    public class WorldGenerator : IWorldGenerator {
        private World _world;
        private const string ZombieId = "Zombie";
        private List<HealthComponent> _enemies = new List<HealthComponent>();
        private TimeManager _timeManager = new TimeManager();

        public void Generate(World world) {
            _world = world;
            GenerateEnemies();

            _timeManager.RepeatCall(1.0f, UpdateWorld);
        }

        private void UpdateWorld() {
            if (_enemies.Count < 3) {
                SpawnEnemy();
            }
        }

        private void GenerateEnemies() {
            var amount = Random.Range(3, 5);
            for (int i = 0; i < amount; i++) {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy() {
            var entity = _world.CreateEntity<Entity>(ZombieId, position: GetRandomPosition());

            if (entity.GetComponentByType<HealthComponent>() != null) {
                var healthComponent = (HealthComponent) entity.GetComponentByType<HealthComponent>();
                _enemies.Add(healthComponent);
                healthComponent.OnDead.AddOnce(health => {
                    _enemies.Remove(health);
                });
            }
        }

        private Vector3 GetRandomPosition() {
            var sizeX = _world.Size.x / 2 - 5;
            var sizeZ = _world.Size.z / 2 - 5;
            var position = GetPosition();
            if (!CheckPlayerDistance()) {
                for (int i = 0; i < 10; i++) {
                    position = GetPosition();
                    if (CheckPlayerDistance()) {
                        break;
                    }
                }

            }

            return position;

            Vector3 GetPosition() {
                return  new Vector3(Random.Range(-sizeX, sizeX), 0, Random.Range(-sizeZ, sizeZ));
            }

            bool CheckPlayerDistance() {
                return Vector3.Distance(_world.Player.Position, position) < 2;
            }
        }
    }
}