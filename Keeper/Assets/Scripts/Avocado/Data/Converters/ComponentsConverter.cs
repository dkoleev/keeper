using System;
using Avocado.Core.Factories;
using Avocado.Core.Factories.Components;
using Avocado.Game.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Converters {
    public class ComponentsConverter : JsonConverter<IComponentData> {
        private Factory<IComponentData> _factory;
        public ComponentsConverter() {
            _factory = new Factory<IComponentData>();
        }

        public override void WriteJson(JsonWriter writer, IComponentData value, JsonSerializer serializer) {
            writer.WriteValue(value.ToString());
        }

        public override IComponentData ReadJson(JsonReader reader, Type objectType, IComponentData existingValue, bool hasExistingValue, JsonSerializer serializer) {
            var item = JObject.Load(reader);
            var type = item["Type"].Value<string>();

            var componentData = _factory.Create(type, item);
            
            return componentData;
        }
    }
}