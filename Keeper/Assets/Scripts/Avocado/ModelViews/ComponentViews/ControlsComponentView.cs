using Avocado.Core.Factories;
using Avocado.Core.Factories.Components;
using Avocado.Models.Components;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.PlayerControls)]
    public class ControlsComponentView : BaseComponentView<ControlsComponent> {
        private readonly int _idleAnimationKey = Animator.StringToHash("Idle");
        private readonly int _walkAnimationKey = Animator.StringToHash("Walk");
        
        public ControlsComponentView(ControlsComponent componentModel, EntityView entityView) : base(componentModel, entityView) {
            Model.OnMove.AddListener(isMove => {
                EntityView.Animator.SetTrigger(isMove ? _walkAnimationKey : _idleAnimationKey);
            });
        }

        public override void Update() {
            base.Update();
            Rotate(EntityView.RotateTransform, Model.RotationSpeed);
            Move();
        }

        private void Move() {
            EntityView.transform.position = Model.Entity.Position;
        }

        private void Rotate(Transform target, float speed) {
            if (Model.MoveAxis.magnitude < 0.001f) {
                return;
            }

            var move = new Vector3(Model.MoveAxis.x, 0, Model.MoveAxis.y);
            if (move.magnitude > 1f) {
                move.Normalize();
            }

            var forward = target.forward;
            var angleCurrent = Mathf.Atan2( forward.x, forward.z) * Mathf.Rad2Deg;
            var targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            var deltaAngle = Mathf.DeltaAngle(angleCurrent, targetAngle);
            var targetLocalRot = Quaternion.Euler(0, deltaAngle, 0);
            var targetRotation = Quaternion.Slerp(Quaternion.identity, targetLocalRot, speed * Time.deltaTime);

            target.rotation *= targetRotation;
        }
    }
}