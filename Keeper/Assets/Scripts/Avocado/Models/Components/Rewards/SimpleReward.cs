using System;
using System.Collections.Generic;
using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using JetBrains.Annotations;

namespace Avocado.Models.Components.Rewards {
    [UsedImplicitly]
    [ObjectType(RewardTypes.Simple)]
    public class SimpleReward : IReward {
        public IReadOnlyDictionary<string, int> Content { get; }

        public SimpleReward(RewardComponent reward) {
            if (reward.Data.Reward is Data.Components.Reward.SimpleReward simpleReward) {
                Content = simpleReward.Content;
            } else {
                throw new Exception("Not found data for simple reward model");
            }
        }
    }
}