using UnityEngine;
using UnityEngine.InputSystem;

namespace Avocado.Controllers {
    public class InputManager : MonoBehaviour {
      /*  public Vector2 MoveAxis => _moveAxis;
        public float RotationYAxis => _rotationYAxis;

        private Controls _controls;
        private Vector2 _moveAxis;
        private float _rotationYAxis;

        private void Awake() {
            _controls = new Controls();

            _controls.Player.Move.performed += HandleMove;
            _controls.Player.Move.canceled += context => _moveAxis = Vector2.zero;
            _controls.Player.Look.performed += HandleLook;
        }

        private void OnEnable() {
            _controls.Player.Enable();
            // _controls.Player.Move.Enable();
            // _controls.Player.Look.Enable();
        }

        private void OnDisable() {
            _controls.Player.Disable();
            // _controls.Player.Move.Disable();
            //  _controls.Player.Look.Disable();
        }

        private void HandleMove(InputAction.CallbackContext context) {
            _moveAxis = context.ReadValue<Vector2>();
        }

        private void HandleLook(InputAction.CallbackContext context) {
            _rotationYAxis = context.ReadValue<float>();
        }
        */
    }
}