using System;
using System.Collections.Generic;
using Avocado.Core;
using Avocado.Game.Components;
using Avocado.Game.Data;
using UnityEngine;

namespace Avocado.Game.Entities {
    public class Entity : MonoBehaviourWrapper {
        [SerializeField]
        private List<string> _currentComponents = new List<string>(3);
        private readonly List<IComponent> _components = new List<IComponent>(3);
        
        public Transform RotateTransform { get; private set; }
        public Transform MoveTransform { get; private set; }
        public Animator Animator { get; private set; }

        public GameData GameData { get; private set; }
        public EntityData EntityData { get; private set; }
        public string EntityId { get; private set; }

        public virtual void Initialize(string entityId, in EntityData entityData, in GameData gameData) {
            GameData = gameData;
            EntityData = entityData;
            EntityId = entityId;
            
            if (!string.IsNullOrEmpty(EntityData.Parent)) {
                AddComponents(EntityData, GameData.Entities.Entities[EntityData.Parent]);
            } else {
                AddComponents(EntityData);
            }

            InitializeComponents();

            Animator = GetComponentInChildren<Animator>();
            var mainTransform = transform;
            RotateTransform = Animator == null ? mainTransform : Animator.transform;
            MoveTransform = mainTransform;
        }
        
        protected override void Update() {
            base.Update();
            UpdateComponents();
        }

        public IComponent GetComponentByType<T>() where T : IComponent{
            foreach (var component in _components) {
                if (component.GetType() == typeof(T)) {
                    return component;
                }
            }

            return null;
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
            _components.Add(component);
            _currentComponents.Add(componentType.ToString());
        }

        private void InitializeComponents() {
            foreach (var component in _components) {
                component.Initialize();
            }
        }

        private void UpdateComponents() {
            var tempComponents = _components.ToArray();
            foreach (var component in tempComponents) {
                component.Update();
            }
        }
    }
}