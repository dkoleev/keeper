using Avocado.Framework.Patterns.AbstractFactory;
using Avocado.Game.Components;
using Avocado.Game.Data;
using Avocado.Game.Systems;

namespace Avocado.Game.Entities
{
    public class Entity : MonoBehaviourWrapper
    {
        public void Create(GameData gameData, EntityData entityData) {
            EntityData parentEntityData = null;
            if (!string.IsNullOrEmpty(entityData.Parent)) {
                parentEntityData = gameData.Entities.Entities[entityData.Parent];
            }
            AddComponents(entityData, parentEntityData);
        }

        private void AddComponents(EntityData data, EntityData parentData) {
            if (parentData != null) {
                foreach (var componentData in parentData.Components) {
                    if (!data.Components.ContainsKey(componentData.Key)) {
                        AddComponent(Factory<IComponent>.Create(componentData.Key.ToString()), componentData.Value);
                    }
                }
            }

            foreach (var componentData in data.Components) {
               AddComponent(Factory<IComponent>.Create(componentData.Key.ToString()), componentData.Value);
            }
        }

        private void AddComponent(IComponent component, IComponentData data) { 
            component.Initialize(this, data);
            ComponentsSystem.AddComponent(component);
        }
        
        public void Destroy() {
            ComponentsSystem.RemoveEntityComponents(this);
        }
    }
}