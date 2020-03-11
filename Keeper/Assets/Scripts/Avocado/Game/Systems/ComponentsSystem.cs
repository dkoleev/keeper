using System;
using System.Collections.Generic;
using Avocado.Framework.Optimization.BatchUpdateSystem;
using Avocado.Game.Components;
using Avocado.Game.Data;

namespace Avocado.Game.Systems {
    public class ComponentsSystem : BaseSystem, IBatchUpdated {
        private Dictionary<Type, List<ComponentBase>> _components = new Dictionary<Type, List<ComponentBase>>();

        public ComponentsSystem(GameData data) : base(data) { }
        public override void Initialize() {
            RegisterAsButchUpdated();
        }

        public void AddComponent(ComponentBase component) {
            var type = component.GetType();
            if (_components.ContainsKey(type)) {
                _components[component.GetType()].Add(component);
            } else {
                _components.Add(type, new List<ComponentBase> {
                    component
                });
            }
        }

        public void RegisterAsButchUpdated() {
            BatchUpdateSystem.Instance.RegisterSlicedUpdate(this, BatchUpdateSystem.UpdateMode.Always);
        }

        public void BatchUpdate() {
            foreach (var components in _components.Values) {
                foreach (var component in components) {
                    component.Update();
                }
            }
        }
    }
}