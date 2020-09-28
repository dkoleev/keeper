using Avocado.Game.Controllers;
using Avocado.Game.Data;
using Avocado.Game.Data.Components;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using Sigtrap.Relays;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ComponentType(ComponentType.PlayerControls)]
    public class ControlsComponent : ComponentBase<PlayerControlsComponentData>
    {
        public float RotationSpeed => _moveComponent.SpeedRotate;
        public Vector2 MoveAxis => _moveAxis;
        
        public Relay<bool> OnMove = new Relay<bool>();
        
        private readonly Controls _controls;
        private Vector2 _moveAxis;
        private float _RotationAxisY;
        private bool _mooving;
     
        private MoveComponent _moveComponent;
        private AttackComponent _attackComponent;
        
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
            _attackComponent= (AttackComponent)Entity.GetComponentByType<AttackComponent>();
            
            Initialized = true;
        }

        public override void Update() {
            base.Update();
            Move();
        }

        private void MoveOnCanceled(InputAction.CallbackContext obj) {
            _moveAxis = Vector2.zero;
        }

        private void MoveOnPerformed(InputAction.CallbackContext context) {
            _moveAxis = context.ReadValue<Vector2>();
        }

        private void Move() {
            if (!Initialized)
                return;

            _moveComponent.CurrentSpeedMove = _moveAxis.magnitude;
            if (_moveAxis.magnitude > 0) {
                if (!_mooving) {
                    _mooving = true;
                    OnMove.Dispatch(true);
                }

                Entity.Position += new Vector3(
                    _moveAxis.x * Time.deltaTime * _moveComponent.SpeedMove,
                    0,
                    _moveAxis.y * Time.deltaTime * _moveComponent.SpeedMove);
            } else if (_mooving) {
                _mooving = false;
                OnMove.Dispatch(false);
            }
        }
    }
}