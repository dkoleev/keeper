using System;
using System.Collections.Generic;
using Avocado.Game.Data;
using Avocado.Models.Components;
using Avocado.Models.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Avocado.Models.Worlds {
    public class World {
        public GameData GameData { get; }
        
        private readonly List<Entity> _entities = new List<Entity>();
        private IWorldGenerator _generator;
        
        public World(in GameData gameData) {
            GameData = gameData;
            _generator = new WorldGeneratorLogDecorator(new WorldGenerator());
        }

        public void Create() {
            CreateEntity<PlayerEntity>("Player");
            _generator.Generate(this);
        }

        public void CreateEntity<T>(string entityId, Vector3 startPosition, Transform parent = null, Action<Entity> onCreate = null)
            where T : Entity {
            var entityData = GameData.Entities.Entities[entityId];
            Addressables.InstantiateAsync(entityData.Prefab, startPosition, Quaternion.identity, parent).Completed += OnLoad;
            void OnLoad(AsyncOperationHandle<GameObject> handle) {
                var go = handle.Result;
                var entity = go.AddComponent<T>();
                entity.Initialize(entityId, entityData, this);
                _entities.Add(entity);
                
                onCreate?.Invoke(entity);
            }
        }
        
        public void CreateEntity(
            string entityId,
            Vector3 startPosition,
            Transform parent = null,
            Action<Entity> onCreate = null) {
            CreateEntity<Entity>(entityId, startPosition, parent, onCreate);
        }

        public void CreateEntity<T>(string entityId, Action<Entity> onCreate = null)
            where T : Entity{
            CreateEntity<T>(entityId, Vector3.zero, null, onCreate);
        }
        
        public void CreateEntity( string entityId, Action<Entity> onCreate = null){
            CreateEntity<Entity>(entityId, Vector3.zero, null, onCreate);
        }

        public IReadOnlyList<Entity> GetEntitiesWithComponent<T>() where T : IComponent {
            var result = new List<Entity>();
            foreach (var entity in _entities) {
                if (entity.GetComponentByType<T>() is null) {
                    continue;
                }
                
                result.Add(entity);
            }

            return result;
        }
    }
}