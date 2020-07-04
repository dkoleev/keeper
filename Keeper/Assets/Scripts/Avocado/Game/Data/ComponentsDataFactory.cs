using Avocado.Game.Data.Components;
using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data {
    public static class ComponentsDataFactory {
        public static IComponentData Create(ComponentType type, JObject data) {
            switch (type) {
                case ComponentType.Move:
                    return new MoveComponentData(data);

                case ComponentType.Health:
                    return new HealthComponentData(data);
                
                case ComponentType.PlayerControls:
                    return new PlayerControlsComponentData();
                
                case ComponentType.Attack:
                    return new AttackComponentData(data);
                
                case ComponentType.Weapon:
                    return new WeaponComponentData(data);
                
                case ComponentType.AI:
                    return new AiComponentData(data);
            }

            return null;
        }
    }
}