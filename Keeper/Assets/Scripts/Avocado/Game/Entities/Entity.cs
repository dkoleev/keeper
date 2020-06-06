using System;
using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Worlds;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Avocado.Game.Entities
{
    public class Entity : MonoBehaviourWrapper {
        public Transform RotateTransform { get; private set; }
        public Transform MoveTransform { get; private set; }
        public Animator Animator { get; private set; }
        public GameData GameData { get; private set; }

        public static void Create(GameData data, string entityId, Vector3 startPosition, Transform parent = null, Action<Entity> onCreate = null) {
            var entityData = data.Entities.Entities[entityId];
            Addressables.InstantiateAsync(entityData.Prefab, startPosition, Quaternion.identity, parent).Completed += onLoad;

            void onLoad(AsyncOperationHandle<GameObject> handle) {
                var go = handle.Result;
                var entity = go.AddComponent<Entity>();
                entity.Initialize(entityData, data);

                onCreate?.Invoke(entity);
            }
        }

        public static void Create(GameData data, string entityId, Action<Entity> onCreate = null) {
            Create(data, entityId, Vector3.zero, null, onCreate);
        }

        private void Initialize(in EntityData entityData, in GameData gameData) {
            GameData = gameData;
            Animator = GetComponentInChildren<Animator>();
            RotateTransform = Animator == null ? transform : Animator.transform;
            MoveTransform = transform;
            
            if (!string.IsNullOrEmpty(entityData.Parent)) {
                AddComponents(entityData, gameData.Entities.Entities[entityData.Parent]);
            } else {
                AddComponents(entityData);
            }
        }

        private void AddComponents(in EntityData data) {
            foreach (var componentData in data.Components) {
                AddComponent(componentData.Key, componentData.Value);
            }
        }

        private void AddComponents(in EntityData data, in EntityData parentData) {
            foreach (var componentData in parentData.Components) {
                if (!data.Components.ContainsKey(componentData.Key)) {
                    AddComponent(componentData.Key, componentData.Value);
                }
            }

            AddComponents(data);
        }

        private void AddComponent(ComponentType componentType, IComponentData data) {
            var component = ComponentsFactory<IComponent>.Create(componentType, this, data);
            World.AddComponent(component);
        }

        public void Destroy() {
            World.RemoveEntityComponents(this);
        }
    }
}