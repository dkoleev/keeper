using System;

namespace Avocado.Core.Factories {
    public class ObjectTypeAttribute : Attribute {
        public string Type { get; private set; }

        public ObjectTypeAttribute(string type) {
            Type = type;
        }
    }
}
