using System.Collections.Generic;
using Avocado.Framework.Patterns.EventSystem;
using Avocado.Game.Components;
using Avocado.Game.Controllers;
using Avocado.Game.Data;
using Avocado.Game.Events;
using Avocado.Game.Worlds;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Avocado.Game.Systems
{
    public class MoveByControlsSystem : BaseSystem
    {
        private Controls _controls;
        private Vector2 _moveAxis;
        private float m_RotationAxisY;
        private bool _initialized;
        private bool _mooving;
        private readonly int _speedMoveAnimationKey = Animator.StringToHash("SpeedMove");
        private List<(ControlsComponent component1, MoveComponent component2)> _components = new List<(ControlsComponent, MoveComponent)>();
        
        public MoveByControlsSystem(GameData data) : base(data)
        {
        }

        public override void Initialize()
        {
            _moveAxis = Vector2.zero;
            _controls = new Controls();

            _controls.Player.Move.performed += MoveOnPerformed;
            _controls.Player.Move.canceled += MoveOnCanceled;
            
            _controls.Player.Enable();

            _components = World.GetComponents<ControlsComponent, MoveComponent>();
            EventSystem<ComponentsUpdatedEvent>.Subscribe(coEvent =>  _components = World.GetComponents<ControlsComponent, MoveComponent>());

            _initialized = true;
        }

        public override void Update()
        {
            foreach (var components in _components) {
                Move(components.component1, components.component2);
            }
        }
        
        private void MoveOnCanceled(InputAction.CallbackContext obj)
        {
            _moveAxis = Vector2.zero;
        }

        private void MoveOnPerformed(InputAction.CallbackContext context)
        {
            _moveAxis = context.ReadValue<Vector2>();
        }

        private void Move(ControlsComponent controls, MoveComponent move) {
            if(!_initialized)
                return;
            
            if (_moveAxis != Vector2.zero) {
                if (!_mooving) {
                    _mooving = true;
                }

                controls.MoveTransform.position += new Vector3(_moveAxis.x * Time.deltaTime * move.SpeedMove, 0, _moveAxis.y * Time.deltaTime * move.SpeedMove);
            }
            else
            {
                _mooving = false;
            }

            if (_mooving) {
                var speed =(Mathf.Abs(_moveAxis.x) + Mathf.Abs(_moveAxis.y));
                controls.Animator.SetFloat(_speedMoveAnimationKey, speed);
            } else {
                controls.Animator.SetFloat(_speedMoveAnimationKey, 0);
            }

            Rotate(controls, move);
        }

        private void Rotate(ControlsComponent controlsComponent, MoveComponent moveComponent) {
            if (_moveAxis.magnitude < 0.001f) {
                return;
            }

            var move = new Vector3(_moveAxis.x, 0, _moveAxis.y);
            if (move.magnitude > 1f) {
                move.Normalize();
            }
            
            var angleCurrent = Mathf.Atan2( controlsComponent.RotateTransform.forward.x, controlsComponent.RotateTransform.forward.z) * Mathf.Rad2Deg;
            var targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            var deltaAngle = Mathf.DeltaAngle(angleCurrent, targetAngle);
            var targetLocalRot = Quaternion.Euler(0, deltaAngle, 0);
            var targetRotation = Quaternion.Slerp(Quaternion.identity, targetLocalRot, moveComponent.SpeedRotate * Time.deltaTime);
            
            controlsComponent.RotateTransform.rotation *= targetRotation;
        }
    }
}