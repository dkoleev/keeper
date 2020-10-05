using System;
using System.Collections.Generic;
using System.Reflection;
using Avocado.Core.Factories.Components;
using Avocado.Data;
using Avocado.Game.Data;
using Avocado.Models.Components;

namespace Avocado.ModelViews.ComponentViews {
    public class ComponentsViewFactory<T> where T : class {
        private readonly ComponentBaseFactory<T> _baseFactory;
        
        public ComponentsViewFactory() {
            _baseFactory = new ComponentBaseFactory<T>();
            _baseFactory.Initialize();
        }
        
        public T Create(IComponent component, EntityView entityView) {
            var type = component.GetType().GetCustomAttribute<ComponentTypeAttribute>().Type;
            
            if (!_baseFactory.Types.ContainsKey(type)) {
                throw new KeyNotFoundException("Not found key for type " + type);
            }

            return (T)Activator.CreateInstance(_baseFactory.Types[type], component, entityView);
        }
    }
}
