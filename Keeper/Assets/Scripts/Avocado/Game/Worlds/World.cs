using System;
using System.Collections.Generic;
using Avocado.Framework.Patterns.EventSystem;
using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Entities;
using Avocado.Game.Events;

namespace Avocado.Game.Worlds {
    public class World {
        /*public static readonly List<MoveComponent> MoveComponents = new List<MoveComponent>();
        public static readonly List<DamageComponent> DamageComponents = new List<DamageComponent>();
        public static readonly List<HealthComponent> HealthComponents = new List<HealthComponent>();
        public static readonly List<ControlsComponent> ControlsComponents = new List<ControlsComponent>();*/

        private static readonly Dictionary<Type, List<IComponent>> Components = new Dictionary<Type, List<IComponent>>();

        public World(GameData data)
        {
            
        }

        public static void AddComponent(IComponent component, ComponentType type) {
            switch (type) {
                case ComponentType.Move:
                    if (Components.ContainsKey(typeof(MoveComponent))) {
                        Components[typeof(MoveComponent)].Add(component);
                    } else {
                        Components.Add(typeof(MoveComponent), new List<IComponent>{component});
                    }
                    break;
                case ComponentType.Damage:
                    if (Components.ContainsKey(typeof(DamageComponent))) {
                        Components[typeof(DamageComponent)].Add(component);
                    } else {
                        Components.Add(typeof(DamageComponent), new List<IComponent>{component});
                    }
                    break;
                case ComponentType.Health:
                    if (Components.ContainsKey(typeof(HealthComponent))) {
                        Components[typeof(HealthComponent)].Add(component);
                    } else {
                        Components.Add(typeof(HealthComponent), new List<IComponent>{component});
                    }
                    break;
                case ComponentType.PlayerControls:
                    if (Components.ContainsKey(typeof(ControlsComponent))) {
                        Components[typeof(ControlsComponent)].Add(component);
                    } else {
                        Components.Add(typeof(ControlsComponent), new List<IComponent> {component});
                    }
                    break;
                case ComponentType.Weapon:
                    if (Components.ContainsKey(typeof(WeaponComponent))) {
                        Components[typeof(WeaponComponent)].Add(component);
                    } else {
                        Components.Add(typeof(WeaponComponent), new List<IComponent> {component});
                    }
                    break;
            }
            
            EventSystem<ComponentsUpdatedEvent>.Fire();
            /*if (component is MoveComponent moveComponent) {
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
            }*/
        }

        public static List<IComponent> GetComponentsByType(Type componentType) {
            return Components[componentType];
        }
        
        public static List<TComponent> GetComponents<TComponent>()
            where TComponent : IComponent {
            List<TComponent> result = new List<TComponent>();
            var components1 = Components[typeof(TComponent)];

            foreach (var component in components1) {
                result.Add((TComponent)component);
            }

            return result;
        }

        public static List<(TComponent1 component1, TComponent2 component2)> GetComponents<TComponent1, TComponent2>()
            where TComponent1 : IComponent
            where TComponent2 : IComponent {
            List<(TComponent1, TComponent2)> result = new List<(TComponent1, TComponent2)>();
            var components1 = Components[typeof(TComponent1)];
            var components2 = Components[typeof(TComponent2)];
            
            foreach (var component1 in components1) {
                foreach (var component2 in components2) {
                    if (component1.Entity == component2.Entity) {
                        result.Add(((TComponent1)component1, (TComponent2)component2));
                        break;
                    }
                }
            }

            return result;
        }
        
        public static List<(TComponent1 component1, TComponent2 component2, TComponent3 component3)> GetComponents<TComponent1, TComponent2, TComponent3>()
            where TComponent1 : IComponent
            where TComponent2 : IComponent 
            where TComponent3 : IComponent{
            List<(TComponent1, TComponent2, TComponent3)> result = new List<(TComponent1, TComponent2, TComponent3)>();
            var components1 = Components[typeof(TComponent1)];
            var components2 = Components[typeof(TComponent2)];
            var components3 = Components[typeof(TComponent3)];
            
            foreach (var component1 in components1) {
                foreach (var component2 in components2) {
                    if (component1.Entity == component2.Entity) {
                        foreach (var component3 in components3) {
                            if (component2.Entity == component3.Entity) {
                                result.Add(((TComponent1)component1, (TComponent2)component2, (TComponent3)component3));
                                break;
                            }
                        }
                        break;
                    }
                }
            }

            return result;
        }

        public static TComponent GetComponentForEntity<TComponent>(Entity entity) where TComponent : IComponent {
            var res = Components[typeof(TComponent)].Find(component => component.Entity == entity);
            if (res == null) {
                
            }

            return (TComponent)Components[typeof(TComponent)].Find(component => component.Entity == entity);
        }

        public static void RemoveEntityComponents(Entity entity) {
            foreach (var components in Components.Values) {
                components.RemoveAll(component => component.Entity == entity);
            }
            
            EventSystem<ComponentsUpdatedEvent>.Fire();
        }
    }
}