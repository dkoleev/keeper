using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data.Components {
    public static class ComponentsDataFactory {
        public static IComponentData Create(ComponentType type, JObject data) {
            switch (type) {
                case ComponentType.Move:
                    var speedMove = data["SpeedMove"].Value<byte>();
                    var speedRotate = data["SpeedRotate"].Value<byte>();
                    return new MoveComponentData(speedMove, speedRotate);
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