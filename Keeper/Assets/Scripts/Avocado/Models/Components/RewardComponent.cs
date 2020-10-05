using System;
using System.Collections.Generic;
using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Data.Components;
using Avocado.Data.Components.Reward;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using Sigtrap.Relays;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.Reward)]
    public class RewardComponent : ComponentBase<RewardData> {
        public Relay<IReadOnlyDictionary<string, int>> OnAward = new Relay<IReadOnlyDictionary<string, int>>();
        
        public RewardComponent(string type, Entity entity, RewardData data) : base(type, entity, data) {
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
            if (Data.Reward is SimpleReward simpleReward) {
                foreach (var rewardItem in simpleReward.Content) {
                    Entity.World.CreateEntity(rewardItem.Key, position: Entity.Position);
                }
                OnAward.Dispatch(simpleReward.Content);
            }
        }
    }
}