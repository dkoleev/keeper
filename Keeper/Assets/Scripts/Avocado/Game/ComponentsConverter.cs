using System;
using Avocado.Game.Data;
using Newtonsoft.Json;

namespace Avocado.Game {
    public class ComponentsConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var comp = value as ComponentData;
            writer.WriteValue(comp);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var component = reader.Value as ComponentData;
            switch (component.Type) {
                case "Health":
                    var health = new HealthData();
                    health = component as HealthData;
                    return new HealthData();
                    break;
                default:
                    return new HealthData();
            }
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(ComponentData);
        }
    }
}
