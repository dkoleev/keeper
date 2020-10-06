using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Avocado.Core.Factories {
    public class Factory<T> where T : class {
        private Dictionary<string, Type> Types { get; }

        public Factory() {
            Types = new Dictionary<string, Type>();
            Initialize();
        }

        private void Initialize() {
            var temp = Assembly.GetAssembly(typeof(T)).GetTypes().Where(mType =>
                !mType.IsAbstract &&
                (mType.IsSubclassOf(typeof(T)) || mType.GetInterfaces().Contains(typeof(T)))
            ).ToList();

            foreach (var type in temp) {
                var attr = type.GetCustomAttribute<ObjectTypeAttribute>();
                if (attr != null) {
                    Types.Add(attr.Type, type);
                }
            }
        }

        public T Create(string type, params Object[] data) {
            if (!Types.ContainsKey(type)) {
                throw new KeyNotFoundException("Not found key for type " + type + " in " + typeof(T));
            }

            var result = (T)Activator.CreateInstance(Types[type], data);

            return result;
        }
    }
}