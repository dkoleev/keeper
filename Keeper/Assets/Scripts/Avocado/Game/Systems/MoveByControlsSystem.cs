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

            Rotate(controls);
        }

        private void Rotate(ControlsComponent component) {
            var h1 = _moveAxis.x;
            var v1 = _moveAxis.y;

            if (Mathf.Abs(h1) > 0 || Mathf.Abs(v1) > 0) {
                /*var curRot = new Vector3 (0, component.RotateTransform.localEulerAngles.y, 0); 
                var newRot = new Vector3 (0f, Mathf.Atan2 (h1, v1) * 180 / Mathf.PI, 0f);
                component.RotateTransform.localEulerAngles = Vector3.Slerp (curRot, newRot, Time.deltaTime*4);*/
                component.RotateTransform.localEulerAngles = new Vector3 (0f, Mathf.Atan2 (h1, v1) * 180 / Mathf.PI, 0f); // this does the actual rotaion according to inputs
            }
        }
    }
}