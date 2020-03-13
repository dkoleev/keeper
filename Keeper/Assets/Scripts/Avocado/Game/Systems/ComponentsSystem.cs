using System.Collections.Generic;
using Avocado.Framework.Optimization.BatchUpdateSystem;
using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Entities;

namespace Avocado.Game.Systems {
    public class ComponentsSystem : BaseSystem, IBatchUpdated {
        private static readonly List<MoveComponent> MoveComponents = new List<MoveComponent>();
        private static readonly List<DamageComponent> DamageComponents = new List<DamageComponent>();
        private static readonly List<HealthComponent> HealthComponents = new List<HealthComponent>();
        private static readonly List<PlayerControlsComponent> PlayerControlsComponents = new List<PlayerControlsComponent>();

        public ComponentsSystem(GameData data) : base(data) { }
        public override void Initialize() {
            RegisterAsButchUpdated();
        }
        
        public void RegisterAsButchUpdated() {
            BatchUpdateSystem.Instance.RegisterSlicedUpdate(this, BatchUpdateSystem.UpdateMode.Always);
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
            if (component is PlayerControlsComponent pcComponent) {
                PlayerControlsComponents.Add(pcComponent);
            }
        }

        public static void RemoveEntityComponents(Entity entity) {
            MoveComponents.RemoveAll(component => component.Entity == entity);
            DamageComponents.RemoveAll(component => component.Entity == entity);
            HealthComponents.RemoveAll(component => component.Entity == entity);
            PlayerControlsComponents.RemoveAll(component => component.Entity == entity);
        }

        public void BatchUpdate() {
            foreach (var component in MoveComponents) {
                component.Update();
            }
            foreach (var component in DamageComponents) {
                component.Update();
            }
            foreach (var component in HealthComponents) {
                component.Update();
            }
            foreach (var component in PlayerControlsComponents) {
                component.Update();
            }
        }
    }
}