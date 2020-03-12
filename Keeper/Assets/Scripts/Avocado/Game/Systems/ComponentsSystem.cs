using System.Collections.Generic;
using Avocado.Framework.Optimization.BatchUpdateSystem;
using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Entities;

namespace Avocado.Game.Systems {
    public class ComponentsSystem : BaseSystem, IBatchUpdated {
        private static List<MoveComponent> _MoveComponents = new List<MoveComponent>();
        private static List<DamageComponent> _DamageComponents = new List<DamageComponent>();
        private static List<HealthComponent> _HealthComponents = new List<HealthComponent>();
        private static List<PlayerControlsComponent> _PlayerControlsComponents = new List<PlayerControlsComponent>();

        public ComponentsSystem(GameData data) : base(data) { }
        public override void Initialize() {
            RegisterAsButchUpdated();
        }

        public static int AddComponent(IComponent component) {
            if (component is MoveComponent moveComponent) {
                _MoveComponents.Add(moveComponent);
                return _MoveComponents.Count - 1;
            }
            if (component is DamageComponent damageComponent) {
                _DamageComponents.Add(damageComponent);
                return _DamageComponents.Count - 1;
            }
            if (component is HealthComponent healthComponent) {
                _HealthComponents.Add(healthComponent);
                return _HealthComponents.Count - 1;
            }
            if (component is PlayerControlsComponent pcComponent) {
                _PlayerControlsComponents.Add(pcComponent);
                return _PlayerControlsComponents.Count - 1;
            }

            return -1;
        }

        public static void RemoveEntityComponents(Entity entity) {
            _MoveComponents.RemoveAll(component => component.Entity == entity);
            _DamageComponents.RemoveAll(component => component.Entity == entity);
            _HealthComponents.RemoveAll(component => component.Entity == entity);
            _PlayerControlsComponents.RemoveAll(component => component.Entity == entity);
        }

        public void RegisterAsButchUpdated() {
            BatchUpdateSystem.Instance.RegisterSlicedUpdate(this, BatchUpdateSystem.UpdateMode.Always);
        }

        public void BatchUpdate() {
            foreach (var component in _MoveComponents) {
                component.Update();
            }
            foreach (var component in _DamageComponents) {
                component.Update();
            }
            foreach (var component in _HealthComponents) {
                component.Update();
            }
            foreach (var component in _PlayerControlsComponents) {
                component.Update();
            }
        }
    }
}