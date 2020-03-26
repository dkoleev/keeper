using System;
using System.Collections.Generic;
using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Entities;

namespace Avocado.Game.Worlds {
    public class World {
        public static readonly List<MoveComponent> MoveComponents = new List<MoveComponent>();
        public static readonly List<DamageComponent> DamageComponents = new List<DamageComponent>();
        public static readonly List<HealthComponent> HealthComponents = new List<HealthComponent>();
        public static readonly List<ControlsComponent> ControlsComponents = new List<ControlsComponent>();

        public World(GameData data)
        {
            
        }

        public static void AddComponent(IComponent component) {
            if (component is MoveComponent moveComponent) {
                MoveComponents.Add(moveComponent);
            }
            if (component is DamageComponent damageComponent) {
                DamageComponents.Add(damageComponent);
            }
            if (component is HealthComponent healthComponent) {
                HealthComponents.Add(healthComponent);
            }
            if (component is ControlsComponent pcComponent) {
                ControlsComponents.Add(pcComponent);
            }
        }

        public static IComponent GetComponentForEntity(Entity entity, Type componentType)
        {
            if (typeof(MoveComponent) == componentType)
                return MoveComponents.Find(component => component.Entity == entity);
            
            if (typeof(DamageComponent) == componentType)
                return DamageComponents.Find(component => component.Entity == entity);
            
            if (typeof(HealthComponent) == componentType)
                return HealthComponents.Find(component => component.Entity == entity);

            if (typeof(ControlsComponent) == componentType)
                return ControlsComponents.Find(component => component.Entity == entity);

            return null;
        }

        public static void RemoveEntityComponents(Entity entity) {
            MoveComponents.RemoveAll(component => component.Entity == entity);
            DamageComponents.RemoveAll(component => component.Entity == entity);
            HealthComponents.RemoveAll(component => component.Entity == entity);
            ControlsComponents.RemoveAll(component => component.Entity == entity);
        }
    }
}