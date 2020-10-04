using System;
using System.Collections.Generic;
using Avocado.Data;
using Avocado.Models.Entities;

namespace Avocado.Models.Components {
    public class ComponentsFactory<T> where T : class {
        private readonly ComponentBaseFactory<T> _baseFactory;
        
        public ComponentsFactory() {
            _baseFactory = new ComponentBaseFactory<T>();
            _baseFactory.Initialize();
        }

        public T Create(ComponentType type, Entity entity, IComponentData data) {
            if (!_baseFactory.Types.ContainsKey(type)) {
                throw new KeyNotFoundException("Not found key for type " + type);
            }

            return (T)Activator.CreateInstance(_baseFactory.Types[type], entity, data);
        }
    }
}
