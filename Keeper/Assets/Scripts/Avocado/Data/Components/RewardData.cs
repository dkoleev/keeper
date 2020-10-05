using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Data.Components.Reward;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Avocado.Data.Components {
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ObjectType(ComponentTypes.Reward)]
    public class RewardData : BaseComponentData {
        public readonly string Trigger;
        public readonly string RewardType;

        [JsonIgnore]
        public readonly IRewardData Reward;

        public RewardData(JObject data) : base(data) {
            var rewardFactory = new Factory<IRewardData>();
            Reward = rewardFactory.Create(RewardType, data);
        }
    }
}
