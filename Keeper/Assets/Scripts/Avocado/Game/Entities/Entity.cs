using System.Collections.Generic;
using UnityEngine;

namespace Avocado.Game.Entities
{
    public class Entity : MonoBehaviourWrapper
    {
        protected List<Component> Components = new List<Component>();

        public virtual void Initialize(List<Component> components)
        {
            Components = components;
        }
    }
}