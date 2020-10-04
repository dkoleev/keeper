using System;
using System.Collections.Generic;
using Avocado.Data;
using Avocado.Data.Components;
using Avocado.Game.Data;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using Sigtrap.Relays;

namespace Avocado.Models.Components.Reward {
    [UsedImplicitly]
    [ComponentType(ComponentType.Reward)]
    public class RewardComponent : ComponentBase<RewardData> {
        public Relay<IReadOnlyDictionary<string, int>> OnAward = new Relay<IReadOnlyDictionary<string, int>>();
        
        public RewardComponent(Entity entity, RewardData data) : base(entity, data) {
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

        public void Award() {
            foreach (var rewardItem in Data.Content) {
                Entity.World.CreateEntity(rewardItem.Key, position: Entity.Position);
            }
            OnAward.Dispatch(Data.Content);
        }
    }
}