using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data.Components {
    public static class ComponentsDataFactory {
        public static IComponentData Create(ComponentType type, JObject data) {
            switch (type) {
                case ComponentType.Move:
                    var speed = data["Speed"].Value<float>();
                    return new MoveComponentData(speed);
                case ComponentType.Damage:
                    var damage = data["Value"].Value<int>();
                    return new DamageComponentData();
                case ComponentType.Health:
                    var health = data["Value"].Value<int>();
                    return new HealthComponentData();
                case ComponentType.PlayerControls:
                    return new PlayerControlsComponentData();
            }

            return null;
        }
    }
}
