using System;
using System.Collections.Generic;
using Avocado.Framework.Patterns.EventSystem;
using Avocado.Game.Components;
using Avocado.Game.Entities;
using Avocado.Game.Events;

namespace Avocado.Game.Worlds {
    public class World {
        private static readonly Dictionary<Type, List<IComponent>> Components = new Dictionary<Type, List<IComponent>>();

        public static void AddComponent(IComponent component) {
            if (Components.ContainsKey(component.GetType())) {
                Components[component.GetType()].Add(component);
            } else {
                Components.Add(component.GetType(), new List<IComponent>{component});
            }

            EventSystem<ComponentsUpdatedEvent>.Fire();
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
            
            if(!Components.ContainsKey(typeof(TComponent1)) || !Components.ContainsKey(typeof(TComponent2))) {
                return result;
            }
            
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
            where TComponent3 : IComponent {
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