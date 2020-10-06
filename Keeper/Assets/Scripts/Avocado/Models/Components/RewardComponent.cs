using System;
using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Data.Components;
using Avocado.Models.Components.Rewards;
using Avocado.Models.Entities;
using Avocado.Models.Triggers;
using JetBrains.Annotations;
using Sigtrap.Relays;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Reward)]
    public class RewardComponent : ComponentBase<RewardData> {
        public Relay<IReward> OnAward = new Relay<IReward>();
        private IReward _reward;
        private Factory<IReward> _rewardFactory;
        private Factory<TriggerBase> _triggerFactory;
        private TriggerBase _trigger;
        
        public RewardComponent(string type, Entity entity, RewardData data) : base(type, entity, data) {
            _rewardFactory = new Factory<IReward>();
            _triggerFactory = new Factory<TriggerBase>();
            _reward = _rewardFactory.Create(Data.RewardType, this);
            _trigger = _triggerFactory.Create(Data.Trigger, Entity);

            _trigger.OnTrigger.AddListener(Award);
        }

        private void Award() {
            foreach (var rewardItem in _reward.Content) {
                var amount = rewardItem.Value;
                while (amount > 0) {
                    Entity.World.CreateEntity(rewardItem.Key, position: Entity.Position);
                    amount--;
                }
            }
            OnAward.Dispatch(_reward);
        }
    }
}