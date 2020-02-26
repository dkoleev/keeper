using System;
using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Avocado.Game {
    public class ComponentsConverter : JsonConverter<ComponentData> {
        public override void WriteJson(JsonWriter writer, ComponentData value, JsonSerializer serializer) {
            writer.WriteValue(value.ToString());
        }

        public override ComponentData ReadJson(JsonReader reader, Type objectType, ComponentData existingValue, bool hasExistingValue, JsonSerializer serializer) {
            var item = JObject.Load(reader);

            switch (item["Type"].ToString()) {
                case "Health":
                    var value = item["Value"].Value<int>();
                    return new HealthComponentData {
                        Value = value
                    };
                case "Damage":
                    value = item["Value"].Value<int>();
                    return new DamageComponentData {
                        Value = value
                    };
            }

            return null;
        }
    }
}
