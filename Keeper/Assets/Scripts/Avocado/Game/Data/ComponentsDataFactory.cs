using Avocado.Game.Data.Components;
using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data {
    public static class ComponentsDataFactory {
        public static IComponentData Create(ComponentType type, JObject data) {
            switch (type) {
                case ComponentType.Move:
                    return new MoveComponentData(data);

                case ComponentType.Health:
                    var maxHealth = data["MaxHealth"].Value<int>();
                    
                    return new HealthComponentData(maxHealth);
                
                case ComponentType.PlayerControls:
                    return new PlayerControlsComponentData();
                
                case ComponentType.Attack:
                    var currentWeapon = data["Weapon"].Value<string>();
                    var startAmmo = data["StartAmmo"].Value<int>();
                    return new AttackComponentData(currentWeapon, startAmmo);
                
                case ComponentType.Weapon:
                    var damage = data["Damage"].Value<float>();
                    var delay = data["Delay"].Value<float>();
                    var clip = data["Clip"].Value<int>();
                    var range = data["Range"].Value<float>();
                    
                    return new WeaponComponentData(damage, delay, clip, range);
            }

            return null;
        }
    }
}