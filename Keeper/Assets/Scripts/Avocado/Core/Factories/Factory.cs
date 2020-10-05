using System;
using System.Collections.Generic;

namespace Avocado.Core.Factories {
    public class Factory<T> where T : class {
        private readonly FactoryInitializer<T> _initializer;
        
        public Factory() {
            _initializer = new FactoryInitializer<T>();
            _initializer.Initialize();
        }

        public T Create(string type, params Object[] data) {
            if (!_initializer.Types.ContainsKey(type)) {
                throw new KeyNotFoundException("Not found key for type " + type);
            }

            return (T)Activator.CreateInstance(_initializer.Types[type], data);
        }
    }
}