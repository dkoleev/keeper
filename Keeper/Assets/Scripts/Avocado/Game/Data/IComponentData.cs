using Avocado.Game.Data.Converters;
using Avocado.Game.Entities;
using Newtonsoft.Json;

namespace Avocado.Game.Data
{
    public enum ComponentType {
        Move,
        Damage,
        Health,
        PlayerControls,
        Weapon
    }
    
    [JsonConverter(typeof(ComponentsConverter))]
    public interface IComponentData
    {
    }
}