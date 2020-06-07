using Avocado.Game.Data.Converters;
using Newtonsoft.Json;

namespace Avocado.Game.Data
{
    public enum ComponentType {
        Move,
        Health,
        PlayerControls,
        Weapon,
        Attack,
        AI
    }
    
    [JsonConverter(typeof(ComponentsConverter))]
    public interface IComponentData
    {
    }
}