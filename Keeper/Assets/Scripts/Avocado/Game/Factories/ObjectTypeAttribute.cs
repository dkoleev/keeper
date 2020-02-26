using System;

namespace Avocado.Game.Factories {
    public class ObjectTypeAttribute : Attribute{
        public string Type { get; }

        public ObjectTypeAttribute(string type) {
            Type = type;
        }
    }
}
