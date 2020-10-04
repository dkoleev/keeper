using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Avocado.Core.Extensions {
    public static class TypeExtensions {
        public static bool IsList(this Type type) {
            return type.IsGenericType && 
                   (type.GetGenericTypeDefinition() == typeof(IList<>) || type.GetGenericTypeDefinition() == typeof(List<>));
        }

        public static bool IsInt(this Type type) {
            return type == typeof(int);
        }
        
        public static bool IsString(this Type type) {
            return type == typeof(string);
        }
        
        public static bool IsBool(this Type type) {
            return type == typeof(bool);
        }
        
        public static bool IsFloat(this Type type) {
            return type == typeof(float);
        }
        
        public static bool IsByte(this Type type) {
            return type == typeof(byte);
        }

        public static bool IsDictionary(this Type type) {
            return type.IsGenericType &&
                   (type.GetGenericTypeDefinition() == typeof(IDictionary<,>) || type.GetGenericTypeDefinition() == typeof(Dictionary<,>));
        }
        
        public static bool IsReadOnlyDictionary(this Type type) {
            return type.IsGenericType && 
                   (type.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>) || type.GetGenericTypeDefinition() == typeof(ReadOnlyDictionary<,>));
        }
    }
}
