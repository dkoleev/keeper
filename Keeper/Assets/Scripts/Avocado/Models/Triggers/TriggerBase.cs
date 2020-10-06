using Avocado.Models.Entities;
using Sigtrap.Relays;

namespace Avocado.Models.Triggers {
    public abstract class TriggerBase {
        public readonly Relay OnTrigger = new Relay();
        protected readonly Entity Entity;

        protected TriggerBase(Entity entity) {
            Entity = entity;
        }
    }
}