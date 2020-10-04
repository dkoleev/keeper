using System.Collections.Generic;
using System.Reflection;
using Avocado.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    public  abstract class BaseComponentData : IComponentData {
        protected BaseComponentData(JObject data) {
            LoadData(data);
        }

        public void LoadData(JObject data) {
            var fields = GetType().GetFields();
            foreach (var field in fields) {
                if (field.GetCustomAttribute<JsonIgnoreAttribute>() != null) {
                    continue;
                }

                if (data[field.Name] is null) {
                    continue;
                }

                var type = field.FieldType;
                if (type.IsInt()) {
                    field.SetValue(this, data[field.Name].Value<int>());
                } else if (type.IsString()) {
                    field.SetValue(this, data[field.Name].Value<string>());
                } else if (type.IsFloat()) {
                    field.SetValue(this, data[field.Name].Value<float>());
                } else if (type.IsBool()) {
                    field.SetValue(this, data[field.Name].Value<bool>());
                } else if (type.IsByte()) {
                    field.SetValue(this, data[field.Name].Value<byte>());
                } else if (type.IsReadOnlyDictionary()) {
                    var keyType = type.GetGenericArguments()[0];
                    var valueType = type.GetGenericArguments()[1];
                    if (valueType.IsInt()) {
                        field.SetValue(this, data[field.Name].ToObject<IReadOnlyDictionary<string, int>>());
                    }else if (valueType.IsString()) {
                        field.SetValue(this, data[field.Name].ToObject<IReadOnlyDictionary<string, string>>());
                    }
                }
            }
        }
    }
}