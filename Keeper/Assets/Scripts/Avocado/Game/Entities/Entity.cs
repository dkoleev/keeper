using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Systems;
using Avocado.Game.Worlds;
using UnityEngine;

namespace Avocado.Game.Entities
{
    public class Entity : MonoBehaviourWrapper
    {
        public void Create(GameData gameData, EntityData entityData)
        {
            EntityData parentEntityData = null;
            if (!string.IsNullOrEmpty(entityData.Parent)) {
                parentEntityData = gameData.Entities.Entities[entityData.Parent];
            }
            AddComponents(entityData, parentEntityData);
        }

        private void AddComponents(EntityData data, EntityData parentData) {
            if (parentData != null) {
                foreach (var componentData in parentData.Components) {
                    if (!data.Components.ContainsKey(componentData.Key)) {
                        AddComponent(componentData.Key, componentData.Value);
                    }
                }
            }

            foreach (var componentData in data.Components) {
                AddComponent(componentData.Key, componentData.Value);
            }
        }

        private void AddComponent(ComponentType componentType, IComponentData data)
        {
            var component = ComponentsFactory<IComponent>.Create(componentType, this, data);
            World.AddComponent(component);
        }
        
        public void Destroy() {
            World.RemoveEntityComponents(this);
        }
    }
}