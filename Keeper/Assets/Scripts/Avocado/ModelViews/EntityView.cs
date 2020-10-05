using System.Collections.Generic;
using Avocado.Core;
using Avocado.Core.Factories;
using Avocado.Models.Entities;
using Avocado.ModelViews.ComponentViews;
using UnityEngine;

namespace Avocado.ModelViews {
    public class EntityView : MonoBehaviourWrapper {
        public Entity Entity { get; private set; }
        public List<IComponentView> Components { get; private set; }
        public WorldView WorldView { get; private set; }
        public Transform RotateTransform { get; private set; }
        public Transform MoveTransform { get; private set; }
        public Animator Animator { get; private set; }

        private Factory<IComponentView> _componentsViewFactory;

        public virtual void Initialize(Entity entity, WorldView worldView) {
            Entity = entity;
            WorldView = worldView;
            Animator = GetComponentInChildren<Animator>();
            var mainTransform = transform;
            RotateTransform = Animator == null ? mainTransform : Animator.transform;
            MoveTransform = mainTransform;
            Components = new List<IComponentView>();
            _componentsViewFactory = new Factory<IComponentView>();

            foreach (var component in Entity.Components) {
                Components.Add(_componentsViewFactory.Create(component.Type, component, this));
            }

            foreach (var component in Components) {
                component.Initialize();
            }
        }

        protected override void Update() {
            base.Update();

            var components = Components.ToArray();
            foreach (var component in components) {
                component.Update();
            }
        }
    }
}