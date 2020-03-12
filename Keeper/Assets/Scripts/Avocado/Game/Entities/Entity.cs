using System;
using System.Collections.Generic;
using Avocado.Framework.Patterns.AbstractFactory;
using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Systems;
using UnityEngine;

namespace Avocado.Game.Entities
{
    public class Entity : MonoBehaviourWrapper
    {
        private GameData _Data;
        private EntityData _entityData;
        private EntityData _parentEntityData;

        public void Create(GameData gameData, EntityData entityData) {
            _Data = gameData;
            _entityData = entityData;
            if (!string.IsNullOrEmpty(entityData.Parent)) {
                _parentEntityData = gameData.Entities.Entities[entityData.Parent];
            }

            AddComponents();
        }

        public void Destroy() {
            ComponentsSystem.RemoveEntityComponents(this);
        }

        private void AddComponents() {
            if (_parentEntityData != null) {
                foreach (var componentData in _parentEntityData.Components) {
                    if (!_entityData.Components.ContainsKey(componentData.Key)) {
                        AddComponent(Factory<IComponent>.Create(componentData.Key), componentData.Value);
                    }
                }
            }

            foreach (var componentData in _entityData.Components) {
                AddComponent(Factory<IComponent>.Create(componentData.Key), componentData.Value);
            }
        }

        private void AddComponent(IComponent component, ComponentData data) { 
            component.Initialize(this, data);
            ComponentsSystem.AddComponent(component);
        }

        private void RemoveComponents() {
            
        }

        private void RemoveComponent() {
            
        }
    }
}