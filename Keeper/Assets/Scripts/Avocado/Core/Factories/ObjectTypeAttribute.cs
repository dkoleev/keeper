using System;

namespace Avocado.Core.Factories {
    public class ObjectTypeAttribute : Attribute {
        public string Type { get; }

        public ObjectTypeAttribute(string type) {
            Type = type;
        }
    }
}
