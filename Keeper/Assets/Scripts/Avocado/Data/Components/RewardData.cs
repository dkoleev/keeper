using Avocado.Data.Components.Reward;
using Avocado.Game.Data;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ComponentType(ComponentType.Reward)]
    public class RewardData : BaseComponentData {
        public readonly string Trigger;
        public readonly string RewardType;
        [JsonIgnore]
        public readonly IReward Reward;

        public RewardData(JObject data) : base(data) {
            //TODO: make factory
            switch (RewardType) {
                case "Simple":
                    Reward = new SimpleReward(data);
                    break;
                case "Inventory":
                    Reward = new InventoryReward();
                    break;
                case "Composite":
                    Reward = new CompositeReward();
                    break;
            }
        }
    }
}
