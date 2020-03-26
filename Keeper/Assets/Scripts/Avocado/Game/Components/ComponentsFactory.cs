using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Avocado.Game.Data;
using Avocado.Game.Entities;

namespace Avocado.Game.Components {
    public class ComponentsFactory<T> where T : class {
        private static readonly Dictionary<ComponentType, Type> _types = new Dictionary<ComponentType, Type>();

        static ComponentsFactory() {
            var temp = Assembly.GetAssembly(typeof(T)).GetTypes().Where(mType =>
                !mType.IsAbstract &&
                (mType.IsSubclassOf(typeof(T)) || mType.GetInterfaces().Contains(typeof(T)))
            ).ToList();

            foreach (var type in temp) {
                var attr = type.GetCustomAttribute<ComponentTypeAttribute>();
                if (attr != null) {
                    _types.Add(attr.Type, type);
                }
            }
        }

        public static T Create(ComponentType type, Entity entity, IComponentData data) {
            if (!_types.ContainsKey(type)) {
                throw new KeyNotFoundException("Not found key for type " + type);
            }

            return (T)Activator.CreateInstance(_types[type], entity, data);
        }
    }
}
