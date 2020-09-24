using Avocado.Core;
using Avocado.Models.Entities;
using UnityEngine;

namespace Avocado.ModelViews {
    public class EntityView : MonoBehaviourWrapper {
        public Entity Entity { get; private set; }
        public Transform RotateTransform { get; private set; }
        public Transform MoveTransform { get; private set; }
        public Animator Animator { get; private set; }

        public virtual void Initialize(Entity entity) {
            Entity = entity;
            Animator = GetComponentInChildren<Animator>();
            var mainTransform = transform;
            RotateTransform = Animator == null ? mainTransform : Animator.transform;
            MoveTransform = mainTransform;
        }
    }
}