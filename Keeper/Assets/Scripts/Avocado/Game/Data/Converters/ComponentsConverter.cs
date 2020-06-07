using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data.Converters {
    public class ComponentsConverter : JsonConverter<IComponentData> {
        public override void WriteJson(JsonWriter writer, IComponentData value, JsonSerializer serializer) {
            writer.WriteValue(value.ToString());
        }

        public override IComponentData ReadJson(JsonReader reader, Type objectType, IComponentData existingValue, bool hasExistingValue, JsonSerializer serializer) {
            var item = JObject.Load(reader);
            var type = item["Type"].ToObject<ComponentType>();
            return ComponentsDataFactory.Create(type, item);
        }
    }
}