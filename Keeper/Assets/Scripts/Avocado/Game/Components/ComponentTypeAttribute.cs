using System;
using Avocado.Game.Data;

namespace Avocado.Game.Components {
    public class ComponentTypeAttribute : Attribute {
        public ComponentType Type { get; private set; }

        public ComponentTypeAttribute(ComponentType type) {
            Type = type;
        }
    }
}
