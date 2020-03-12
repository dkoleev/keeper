using Avocado.Framework.Patterns.AbstractFactory;
using Avocado.Game.Data;
using Avocado.Game.Entities;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ObjectType("Move")]
    public class MoveComponent : IComponent {
        public int MaxSpeed;
        public Entity Entity { get; private set; }
        
        public void Initialize(Entity entity, ComponentData data) {
            Entity = entity;
        }

        public void Update() {
            
        }
    }
}
