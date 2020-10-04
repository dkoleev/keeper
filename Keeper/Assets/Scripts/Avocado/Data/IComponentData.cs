using Avocado.Data.Converters;
using Newtonsoft.Json;

namespace Avocado.Data {
    public enum ComponentType {
        Move,
        Health,
        PlayerControls,
        Weapon,
        Attack,
        AI,
        Inventory,
        Reward
    }

    [JsonConverter(typeof(ComponentsConverter))]
    public interface IComponentData { }
}