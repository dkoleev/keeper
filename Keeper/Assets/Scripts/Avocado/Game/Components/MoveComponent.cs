using Avocado.Game.Factories;
using JetBrains.Annotations;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ObjectType("Move")]
    public class MoveComponent : ComponentBase {
        public int MaxSpeed;
    }
}
