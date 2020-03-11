using System.Collections.Generic;
using Avocado.Framework.Patterns.AbstractFactory;
using Avocado.Game.Components;
using Avocado.Game.Data;
using UnityEngine;

namespace Avocado.Game.Entities
{
    public class Entity : MonoBehaviourWrapper
    {
        [SerializeField]
        protected List<ComponentBase> Components = new List<ComponentBase>();
        private GameData _Data;
        private EntityData _entityData;
        private EntityData _parentEntityData;

        public void Initialize(GameData gameData, EntityData entityData) {
            _Data = gameData;
            _entityData = entityData;
            if (!string.IsNullOrEmpty(entityData.Parent)) {
                _parentEntityData = gameData.Entities.Entities[entityData.Parent];
            }

            AddComponents();
        }

        private void AddComponents() {
            if (_parentEntityData != null) {
                foreach (var componentData in _parentEntityData.Components) {
                    if (!_entityData.Components.ContainsKey(componentData.Key)) {
                        AddComponent(Factory<ComponentBase>.Create(componentData.Key));
                    }
                }
            }

            foreach (var componentData in _entityData.Components) {
                AddComponent(Factory<ComponentBase>.Create(componentData.Key));
            }
        }

        private void AddComponent(ComponentBase component) {
            component.Initialize(this);
            Components.Add(component);
        }
    }
}