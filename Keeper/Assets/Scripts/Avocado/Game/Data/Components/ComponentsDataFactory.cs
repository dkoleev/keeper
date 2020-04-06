using Avocado.Game.Data.Components.Weapons;
using Newtonsoft.Json.Linq;

namespace Avocado.Game.Data.Components {
    public static class ComponentsDataFactory {
        public static IComponentData Create(ComponentType type, JObject data) {
            switch (type) {
                case ComponentType.Move:
                    var speedMove = data["SpeedMove"].Value<byte>();
                    var speedRotate = data["SpeedRotate"].Value<byte>();
                    
                    return new MoveComponentData(speedMove, speedRotate);

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
                    var damage = data["Damage"].Value<int>();
                    var clip = data["Clip"].Value<int>();
                    var range = data["Range"].Value<int>();
                    
                    return new WeaponComponentData(damage, clip, range);
            }

            return null;
        }
    }
}