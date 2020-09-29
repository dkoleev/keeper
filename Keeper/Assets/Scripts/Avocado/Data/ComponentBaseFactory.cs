using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avocado.Game.Data;

namespace Avocado.Data {
    public class ComponentBaseFactory<T> where T : class {
        public Dictionary<ComponentType, Type> Types { get; }

        public ComponentBaseFactory() {
            Types = new Dictionary<ComponentType, Type>();
        }

        public void Initialize() {
            var temp = Assembly.GetAssembly(typeof(T)).GetTypes().Where(mType =>
                !mType.IsAbstract &&
                (mType.IsSubclassOf(typeof(T)) || mType.GetInterfaces().Contains(typeof(T)))
            ).ToList();

            foreach (var type in temp) {
                var attr = type.GetCustomAttribute<ComponentTypeAttribute>();
                if (attr != null) {
                    Types.Add(attr.Type, type);
                }
            }
        }
    }
}