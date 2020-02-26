using System;
using Avocado.Game.Data.Converters;
using Newtonsoft.Json;

namespace Avocado.Game.Data
{
    [Serializable]
    [JsonConverter(typeof(ComponentsConverter))]
    public abstract class ComponentData {
        public string Type { get; set; }
        public int Value { get; set; }
    }
}