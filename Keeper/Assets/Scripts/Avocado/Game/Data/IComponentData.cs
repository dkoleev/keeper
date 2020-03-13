using Avocado.Game.Data.Converters;
using Newtonsoft.Json;

namespace Avocado.Game.Data
{
    public enum ComponentType {
        Move,
        Damage,
        Health,
        PlayerControls
    }
    
    [JsonConverter(typeof(ComponentsConverter))]
    public interface IComponentData {
    }
}