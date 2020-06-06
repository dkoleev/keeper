using Avocado.Game.Controllers;
using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Game.Entities;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Avocado.Game.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.PlayerControls)]
    public class ControlsComponent : ComponentBase<PlayerControlsComponentData>
    {
        private Controls _controls;
        private Vector2 _moveAxis;
        private float m_RotationAxisY;
        private bool _mooving;
        private readonly int _speedMoveAnimationKey = Animator.StringToHash("SpeedMove");
        private readonly int _stateAnimationKey = Animator.StringToHash("State");
        private MoveComponent _moveComponent;
        
        public ControlsComponent(Entity entity, PlayerControlsComponentData data) : base(entity, data) {
            _moveAxis = Vector2.zero;
            _controls = new Controls();
            _controls.Player.Move.performed += MoveOnPerformed;
            _controls.Player.Move.canceled += MoveOnCanceled;
            _controls.Player.Enable();
        }

        public override void Initialize() {
            base.Initialize();
            
            _moveComponent= (MoveComponent)Entity.GetComponentByType<MoveComponent>();
            
            Initialized = true;
        }

        public override void Update() {
            base.Update();
            Move();
        }
        
           private void MoveOnCanceled(InputAction.CallbackContext obj)
        {
            _moveAxis = Vector2.zero;
        }

        private void MoveOnPerformed(InputAction.CallbackContext context)
        {
            _moveAxis = context.ReadValue<Vector2>();
        }

        private void Move() {
            if(!Initialized)
                return;

            _moveComponent.CurrentSpeedMove = _moveAxis.magnitude;
            if (_moveAxis.magnitude > 0) {
                if (!_mooving) {
                    _mooving = true;
                }

                _moveComponent.Entity.MoveTransform.position += new Vector3(
                    _moveAxis.x * Time.deltaTime * _moveComponent.SpeedMove, 
                    0, 
                    _moveAxis.y * Time.deltaTime * _moveComponent.SpeedMove);
            }
            else
            {
                _mooving = false;
            }
            
            if (_mooving) {
                Entity.Animator.SetInteger(_stateAnimationKey, 1);
                var speed =(Mathf.Abs(_moveAxis.x) + Mathf.Abs(_moveAxis.y));
                Entity.Animator.SetFloat(_speedMoveAnimationKey, speed);
            } else {
                Entity.Animator.SetInteger(_stateAnimationKey, 0);
                Entity.Animator.SetFloat(_speedMoveAnimationKey, 0);
            }

            Rotate(_moveComponent.Entity.RotateTransform, _moveComponent.SpeedRotate);
        }

        private void Rotate(Transform target, float speed) {
            if (_moveAxis.magnitude < 0.001f) {
                return;
            }

            var move = new Vector3(_moveAxis.x, 0, _moveAxis.y);
            if (move.magnitude > 1f) {
                move.Normalize();
            }
            
            var angleCurrent = Mathf.Atan2( target.forward.x, target.forward.z) * Mathf.Rad2Deg;
            var targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            var deltaAngle = Mathf.DeltaAngle(angleCurrent, targetAngle);
            var targetLocalRot = Quaternion.Euler(0, deltaAngle, 0);
            var targetRotation = Quaternion.Slerp(Quaternion.identity, targetLocalRot, speed * Time.deltaTime);
            
            target.rotation *= targetRotation;
        }
    }
}