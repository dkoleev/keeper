using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.Move)]
    public class MoveComponent : ComponentBase<MoveComponentData> {
        public float SpeedMove => Data.SpeedMove;
        public float CurrentSpeedMove { get; set; }
        public float SpeedRotate => Data.SpeedRotate;
        public Transform RotateTransform { get; }
        public Transform MoveTransform { get; }

        public MoveComponent(Entity entity, MoveComponentData data) : base(entity, data)
        {
            RotateTransform = Entity.GetComponentInChildren<Animator>().transform;
            MoveTransform = Entity.transform;
            CurrentSpeedMove = 0;
        }
    }
}