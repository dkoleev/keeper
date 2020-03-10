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
        protected GameData Data;
        protected EntityData EntityData;
        protected EntityData ParentEntityData;

        public virtual void Initialize(GameData gameData, EntityData entityData) {
            Data = gameData;
            EntityData = entityData;
            if (!string.IsNullOrEmpty(entityData.Parent)) {
                ParentEntityData = gameData.Entities.Entities[entityData.Parent];
            }

            AddComponents();
        }

        private void AddComponents() {
            if (ParentEntityData != null) {
                foreach (var componentData in ParentEntityData.Components) {
                    if (!EntityData.Components.ContainsKey(componentData.Key)) {
                        AddComponent(Factory<ComponentBase>.Create(componentData.Key));
                    }
                }
            }

            foreach (var componentData in EntityData.Components) {
                AddComponent(Factory<ComponentBase>.Create(componentData.Key));
            }
        }

        private void AddComponent(ComponentBase component) {
            component.Initialize(this);
            Components.Add(component);
        }
    }
}