using Avocado.Data.Converters;
using Newtonsoft.Json;

namespace Avocado.Data {
    [JsonConverter(typeof(ComponentsConverter))]
    public interface IComponentData { }
}