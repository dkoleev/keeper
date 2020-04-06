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
                
                case ComponentType.Weapon:
                    var damage = data["Damage"].Value<int>();
                    var ammo = data["Ammo"].Value<int>();
                    var range = data["Range"].Value<int>();
                    var prefab = data["Prefab"].Value<string>();
                    var weaponType = data["WeaponType"].Value<string>();
                    
                    return new WeaponComponentData(weaponType, damage, ammo, range, prefab);
                
                case ComponentType.Attack:
                    var currentWeapon = data["CurrentWeapon"].Value<string>();
                    return new AttackComponentData(currentWeapon);
            }

            return null;
        }
    }
}