using System;
using Newtonsoft.Json;

namespace Avocado.Game.Data
{
    [Serializable]
    [JsonConverter(typeof(ComponentsConverter))]
    public class ComponentData {
    }
}