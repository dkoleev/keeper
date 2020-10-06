using System.Collections.Generic;
using Avocado.Framework.Patterns.Factory;
using Avocado.Data;
using Avocado.Game.Data;
using Avocado.Models.Components;
using Avocado.Models.Worlds;
using UnityEngine;

namespace Avocado.Models.Entities {
    public class Entity {
        public string Id { get; private set; }
        public Entity Parent { get; private set; }
        public List<IComponent> Components => _components;

        private readonly List<IComponent> _components = new List<IComponent>(3);
        public Vector3 Position { get; set; }

        public World World { get; private set; }
        public GameData GameData { get; private set; }
        public EntityData EntityData { get; private set; }

        private readonly Factory<IComponent> _componentsFactory;

        public Entity() {
            Position = Vector3.zero;
            _componentsFactory = new Factory<IComponent>();
        }

        public virtual void Initialize(string entityId, in EntityData entityData, World world, Entity parent = null) {
            World = world;
            EntityData = entityData;
            Id = entityId;
            Parent = parent;
            
            if (!string.IsNullOrEmpty(EntityData.Parent)) {
                AddComponents(EntityData, GameData.Entities.Entities[EntityData.Parent]);
            } else {
                AddComponents(EntityData);
            }
            
            InitializeComponents();
        }

        public void PostInitialize() {
        }

        public void SetPosition(Vector3 position) {
            Position = position;
        }

        public void Update() {
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

        private void AddComponent(string componentType, IComponentData data) {
            var component = _componentsFactory.Create(componentType, componentType, this, data);
            _components.Add(component);
        }

        private void InitializeComponents() {
            foreach (var component in _components) {
                component.Initialize();
            }
        }

        private void UpdateComponents() {
            var tempComponents = _components.ToArray();
            foreach (var component in _components) {
                component.Update();
            }
        }
    }
}