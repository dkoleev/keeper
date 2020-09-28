using Avocado.Game.Data;
using Avocado.Models.Components;
using JetBrains.Annotations;
using UnityEngine;

namespace Avocado.ModelViews.ComponentViews {
    [UsedImplicitly]
    [ComponentType(ComponentType.PlayerControls)]
    public class ControlsComponentView : BaseComponentView {
        private readonly int _idleAnimationKey = Animator.StringToHash("Idle");
        private readonly int _walkAnimationKey = Animator.StringToHash("Walk");
        
        private ControlsComponent ControlsModel;
        
        public ControlsComponentView(ControlsComponent componentModel, EntityView entityView) : base(componentModel, entityView) {
            ControlsModel = componentModel;
            ControlsModel.OnMove.AddListener(isMove => {
                EntityView.Animator.SetTrigger(isMove ? _walkAnimationKey : _idleAnimationKey);
            });
        }

        public override void Update() {
            base.Update();
            Rotate(EntityView.RotateTransform, ControlsModel.RotationSpeed);
            Move();
        }

        private void Move() {
            EntityView.transform.position = ControlsModel.Entity.Position;
        }

        private void Rotate(Transform target, float speed) {
            if (ControlsModel.MoveAxis.magnitude < 0.001f) {
                return;
            }

            var move = new Vector3(ControlsModel.MoveAxis.x, 0, ControlsModel.MoveAxis.y);
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