using System;
using Avocado.Game.Factories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data.Converters {
    public class ComponentsConverter : JsonConverter<ComponentData> {
        public override void WriteJson(JsonWriter writer, ComponentData value, JsonSerializer serializer) {
            writer.WriteValue(value.ToString());
        }

        public override ComponentData ReadJson(JsonReader reader, Type objectType, ComponentData existingValue, bool hasExistingValue, JsonSerializer serializer) {
            var item = JObject.Load(reader);
            var value = item["Value"].Value<int>();
            var type = item["Type"].ToString();
            
            var component = Factory<ComponentData>.Create(type);
            component.Value = value;

            return component;
        }
    }
}