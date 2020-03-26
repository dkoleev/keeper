using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.PlayerControls)]
    public readonly struct ControlsComponent : IComponent
    {
        public Entity Entity { get; }
        public Transform MoveTransform { get; }
        public Transform RotateTransform { get; }
        public Animator Animator { get; }

        private readonly PlayerControlsComponentData _data;

        public ControlsComponent(Entity entity, IComponentData data)
        {
            Entity = entity;
            _data = (PlayerControlsComponentData) data;
            MoveTransform = Entity.transform;
            Animator = Entity.GetComponentInChildren<Animator>();
            RotateTransform = Animator.transform;
        }
    }
}