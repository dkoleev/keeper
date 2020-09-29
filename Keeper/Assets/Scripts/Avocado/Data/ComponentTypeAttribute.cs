using System;
using Avocado.Data;

namespace Avocado.Game.Data {
    public class ComponentTypeAttribute : Attribute {
        public ComponentType Type { get; private set; }

        public ComponentTypeAttribute(ComponentType type) {
            Type = type;
        }
    }
}
