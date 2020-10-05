using System;
using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Data.Components;
using Avocado.Models.Components.Rewards;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using Sigtrap.Relays;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Reward)]
    public class RewardComponent : ComponentBase<RewardData> {
        public Relay<IReward> OnAward = new Relay<IReward>();
        private IReward _reward;
        private Factory<IReward> _rewardFactory;
        
        public RewardComponent(string type, Entity entity, RewardData data) : base(type, entity, data) {
            _rewardFactory = new Factory<IReward>();
            _reward = _rewardFactory.Create(Data.RewardType, this);
            
            switch (Data.Trigger) {
                case "Dead":
                    if (Entity.GetComponentByType<HealthComponent>() is HealthComponent healthComponent) {
                        healthComponent.OnDead.AddOnce(component => {
                            Award();
                        });
                    } else {
                        throw new ArgumentException("Entity has a reward with Dead trigger but has not Health Component");
                    }
                    break;
            }
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