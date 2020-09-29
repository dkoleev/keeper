using System;
using System.Collections.Generic;
using Avocado.Models.Entities;
using Avocado.Models.Worlds;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Avocado.ModelViews {
    public class WorldView {
        public Dictionary<Entity, EntityView> Entities => _entityViews;
        private World _worldModel;
        private Dictionary<Entity, EntityView> _entityViews;

        public WorldView(World worldModel) {
            _worldModel = worldModel;
            _entityViews = new Dictionary<Entity, EntityView>();
        }

        public void Create() {
            foreach (var entity in _worldModel.Entities) {
                if (entity is PlayerEntity) {
                    CreateEntityView<PlayerEntityView>(entity);
                } else {
                    CreateEntityView<EntityView>(entity);
                }
            }
        }
        
        public void CreateEntityView<T>(Entity entity, Transform parent = null, Action<EntityView> onCreate = null)
            where T : EntityView {
            Addressables.InstantiateAsync(entity.EntityData.Prefab, entity.Position, Quaternion.identity, parent).Completed += OnLoad;
            void OnLoad(AsyncOperationHandle<GameObject> handle) {
                var go = handle.Result;
                var view = go.AddComponent<T>();
                view.Initialize(entity, this);
                _entityViews.Add(entity, view);
                
                onCreate?.Invoke(view);
            }
        }
    }
}