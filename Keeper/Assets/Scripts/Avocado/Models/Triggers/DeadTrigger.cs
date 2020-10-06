using Avocado.Core.Factories;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Models.Components;
using Avocado.Models.Entities;
using JetBrains.Annotations;

namespace Avocado.Models.Triggers {
    [UsedImplicitly]
    [ObjectType(TriggerTypes.Dead)]
    public class DeadTrigger : TriggerBase {
        public DeadTrigger(Entity entity) : base(entity) {
            if (Entity.GetComponentByType<HealthComponent>() is HealthComponent health) {
                health.OnDead.AddOnce(component => {
                    OnTrigger.Dispatch();
                });
            }
        }
    }
}