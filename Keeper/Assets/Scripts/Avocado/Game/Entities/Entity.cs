using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Worlds;
using UnityEngine;

namespace Avocado.Game.Entities
{
    public class Entity : MonoBehaviourWrapper {
        public Animator Animator { get; private set; }
        public GameData GameData { get; private set; }

        public static Entity Create(GameData data, string entityId, Vector3 startPosition, Transform parent = null) {
            var entityData = data.Entities.Entities[entityId];
            var entityPrefab = Resources.Load<GameObject>(entityData.Prefab);
            var go = Object.Instantiate(entityPrefab, startPosition, Quaternion.identity, parent);
            var entity = go.AddComponent<Entity>();
            entity.Initialize(entityData, data);

            return entity;
        }

        public static Entity Create(GameData data, string entityId) {
            return Create(data, entityId, Vector3.zero);
        }

        private void Initialize(in EntityData entityData, in GameData gameData) {
            GameData = gameData;
            Animator = GetComponentInChildren<Animator>();
            
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
            World.AddComponent(component, componentType);
        }

        public void Destroy() {
            World.RemoveEntityComponents(this);
        }
    }
}