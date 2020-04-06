using Avocado.Game.Data.Converters;
using Avocado.Game.Entities;
using Newtonsoft.Json;

namespace Avocado.Game.Data
{
    public enum ComponentType {
        Move,
        Health,
        PlayerControls,
        Weapon,
        Attack,
        FireAttack
    }
    
    [JsonConverter(typeof(ComponentsConverter))]
    public interface IComponentData
    {
    }
}