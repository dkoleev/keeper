using Avocado.Core.Controls;
using Avocado.Framework.Patterns.Factory;
using Avocado.Core.Factories.ObjectTypes;
using Avocado.Data.Components;
using Avocado.Models.Entities;
using JetBrains.Annotations;
using Sigtrap.Relays;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Avocado.Models.Components {
    [UsedImplicitly]
    [ObjectType(ComponentTypes.PlayerControls)]
    public class ControlsComponent : ComponentBase<PlayerControlsComponentData>
    {
        public float RotationSpeed => _moveComponent.SpeedRotate;
        public Vector2 MoveAxis => _moveAxis;
        
        public Relay<bool> OnMove = new Relay<bool>();
        
        private Vector2 _moveAxis;
        private float _RotationAxisY;
        private bool _mooving;
     
        private MoveComponent _moveComponent;
        private AttackComponent _attackComponent;
        
        public ControlsComponent(string type, Entity entity, PlayerControlsComponentData data) : base(type, entity, data) {
            _moveAxis = Vector2.zero;
            ControlsManager.Instance.MainControl.Player.Move.performed += MoveOnPerformed;
            ControlsManager.Instance.MainControl.Player.Move.canceled += MoveOnCanceled; 
            ControlsManager.Instance.MainControl.Player.Enable();
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