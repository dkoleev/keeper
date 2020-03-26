using Avocado.Game.Components;
using Avocado.Game.Controllers;
using Avocado.Game.Data;
using Avocado.Game.Worlds;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Avocado.Game.Systems
{
    public class MoveByControlsSystem : BaseSystem
    {
        private Controls _controls;
        public Vector2 _moveAxis;
        private float m_RotationAxisY;
        private bool _initialized;
        private bool _mooving;
        private readonly int _speedMoveAnimationKey = Animator.StringToHash("SpeedMove");
        
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

            _initialized = true;
        }

        public override void Update()
        {
            foreach (var component in World.ControlsComponents)
            {
                var moveComponent = World.GetComponentForEntity(component.Entity, typeof(MoveComponent));
                if(moveComponent is null)
                    continue;
                Move(component, (MoveComponent)moveComponent);
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
            
            if (h1 >= 0f || v1 >= 0f)
            {
                component.RotateTransform.localEulerAngles = new Vector3(0f, Mathf.Atan2(h1, v1) * 180 / Mathf.PI,0f);
            }
        }
    }
}