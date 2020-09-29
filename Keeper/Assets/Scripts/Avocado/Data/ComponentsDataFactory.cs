using System;
using System.Collections.Generic;
using Avocado.Game.Data;
using Newtonsoft.Json.Linq;

namespace Avocado.Data {
    public class ComponentsDataFactory<T> where T : class {
        private readonly ComponentBaseFactory<T> _baseFactory;

        public ComponentsDataFactory() {
            _baseFactory = new ComponentBaseFactory<T>();
            _baseFactory.Initialize();
        }

        public T Create(ComponentType type, JObject data) {
            if (!_baseFactory.Types.ContainsKey(type)) {
                throw new KeyNotFoundException("Not found key for type " + type);
            }

            var constructor = _baseFactory.Types[type].GetConstructor(new[] {data.GetType()});
            if (constructor is null) {
                return (T)Activator.CreateInstance(_baseFactory.Types[type]);
            }
            
            return (T)Activator.CreateInstance(_baseFactory.Types[type], data);
        }
    }
}