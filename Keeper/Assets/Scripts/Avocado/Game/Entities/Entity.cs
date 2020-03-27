﻿using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Worlds;

namespace Avocado.Game.Entities
{
    public class Entity : MonoBehaviourWrapper
    {
        public void Create(EntityData entityData, GameData gameData)
        {
            if (!string.IsNullOrEmpty(entityData.Parent)) {
                EntityData parentEntityData = gameData.Entities.Entities[entityData.Parent];
                AddComponents(entityData, parentEntityData);
            }
            else
            {
                AddComponents(entityData);
            }
        }

        private void AddComponents(in EntityData data)
        {
            foreach (var componentData in data.Components)
            {
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