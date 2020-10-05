using System;

namespace Avocado.Core.Factories.Components {
    public class ComponentTypeAttribute : Attribute {
        public ComponentType Type { get; private set; }

        public ComponentTypeAttribute(ComponentType type) {
            Type = type;
        }
    }
}
