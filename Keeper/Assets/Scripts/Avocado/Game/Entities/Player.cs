using Avocado.Framework.Patterns.AbstractFactory;
using Avocado.Game.Components;
using Avocado.Game.Data;

namespace Avocado.Game.Entities {
    public class Player : Entity {
        public override void Initialize(GameData gameData) {
            base.Initialize(gameData);

            foreach (var componentData in Data.Player.Entity.Components) {
                AddComponent(Factory<ComponentBase>.Create(componentData.Type));
            }
        }
    }
}