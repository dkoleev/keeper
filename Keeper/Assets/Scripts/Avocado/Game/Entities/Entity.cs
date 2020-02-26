using System.Collections.Generic;
using Avocado.Game.Components;
using Avocado.Game.Data;
using UnityEngine;

namespace Avocado.Game.Entities
{
    public class Entity : MonoBehaviourWrapper
    {
        [SerializeField]
        protected List<ComponentBase> Components = new List<ComponentBase>();
        protected GameData Data;

        public virtual void Initialize(GameData gameData) {
            Data = gameData;
        }

        protected void AddComponent(ComponentBase component) {
            component.Initialize(this);
            Components.Add(component);
        }
    }
}