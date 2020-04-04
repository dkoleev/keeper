using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data.Components {
    public static class ComponentsDataFactory {
        public static IComponentData Create(ComponentType type, JObject data) {
            switch (type) {
                case ComponentType.Move:
                    var speed = data["Speed"].Value<byte>();
                    return new MoveComponentData(speed);
                case ComponentType.Damage:
                    var damage = data["Value"].Value<int>();
                    return new DamageComponentData();
                case ComponentType.Health:
                    var maxHealth = data["MaxHealth"].Value<int>();
                    return new HealthComponentData(maxHealth);
                case ComponentType.PlayerControls:
                    return new PlayerControlsComponentData();
            }

            return null;
        }
    }
}
