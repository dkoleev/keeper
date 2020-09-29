using System;
using Avocado.Game.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Converters {
    public class ComponentsConverter : JsonConverter<IComponentData> {
        public override void WriteJson(JsonWriter writer, IComponentData value, JsonSerializer serializer) {
            writer.WriteValue(value.ToString());
        }

        public override IComponentData ReadJson(JsonReader reader, Type objectType, IComponentData existingValue, bool hasExistingValue, JsonSerializer serializer) {
            var item = JObject.Load(reader);
            var type = item["Type"].ToObject<ComponentType>();

            var factory = new ComponentsDataFactory<IComponentData>();
            var componentData = factory.Create(type, item);
            
            return componentData;
        }
    }
}