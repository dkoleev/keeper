using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Avocado.Core.Factories {
    public class FactoryInitializer<T> where T : class {
        public Dictionary<string, Type> Types { get; }

        public FactoryInitializer() {
            Types = new Dictionary<string, Type>();
        }

        public void Initialize() {
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
    }
}
