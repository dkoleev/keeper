using System;
using System.Collections.Generic;
using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Avocado.Game.Worlds {
    public static class World {
        private static readonly List<Entity> _entities = new List<Entity>();

        public static void CreateEntity<T>(GameData data, string entityId, Vector3 startPosition, Transform parent = null, Action<Entity> onCreate = null)
            where T : Entity {
            var entityData = data.Entities.Entities[entityId];
            Addressables.InstantiateAsync(entityData.Prefab, startPosition, Quaternion.identity, parent).Completed += OnLoad;
            void OnLoad(AsyncOperationHandle<GameObject> handle) {
                var go = handle.Result;
                var entity = go.AddComponent<T>();
                entity.Initialize(entityId, entityData, data);
                _entities.Add(entity);
                
                onCreate?.Invoke(entity);
            }
        }
        
        public static void CreateEntity(GameData data,
            string entityId,
            Vector3 startPosition,
            Transform parent = null,
            Action<Entity> onCreate = null) {
            CreateEntity<Entity>(data, entityId, startPosition, parent, onCreate);
        }

        public static void CreateEntity<T>(GameData data, string entityId, Action<Entity> onCreate = null)
            where T : Entity{
            CreateEntity<T>(data, entityId, Vector3.zero, null, onCreate);
        }
        
        public static void CreateEntity(GameData data, string entityId, Action<Entity> onCreate = null){
            CreateEntity<Entity>(data, entityId, Vector3.zero, null, onCreate);
        }

        public static IReadOnlyList<Entity> GetEntitiesWithComponent<T>() where T : IComponent {
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