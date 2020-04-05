using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Move)]
    public class MoveComponent : IComponent {
        public Entity Entity { get; }
        public float SpeedMove => _data.SpeedMove;
        public float CurrentSpeedMove { get; set; }
        public float SpeedRotate => _data.SpeedRotate;
        public Transform RotateTransform { get; }
        public Transform MoveTransform { get; }

        private readonly MoveComponentData _data;
        
        public MoveComponent(Entity entity, IComponentData data)
        {
            Entity = entity;
            _data = (MoveComponentData) data;

            RotateTransform = Entity.GetComponentInChildren<Animator>().transform;
            MoveTransform = Entity.transform;
            CurrentSpeedMove = 0;
        }
    }
}