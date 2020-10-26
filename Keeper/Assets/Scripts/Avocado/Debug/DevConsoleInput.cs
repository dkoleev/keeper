using Avocado.Core.Controls;
using Avocado.DeveloperCheatConsole.Scripts.Visual;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Avocado.Debug {
    public class DevConsoleInput : DevConsoleGUI {
        protected override void Awake() {
            base.Awake();
            ControlsManager.Instance.MainControl.Debug.ToggleDebug.performed += OnToggleDebug;
            ControlsManager.Instance.MainControl.Debug.Return.performed += OnReturn;
            ControlsManager.Instance.MainControl.Debug.PrevCommands.performed += OnPrevCommand;
            ControlsManager.Instance.MainControl.Debug.PrevCommandsBack.performed += OnPrevCommandBack;
            ControlsManager.Instance.MainControl.Debug.ExitDebug.performed += OnExitDebug;
            ControlsManager.Instance.MainControl.Debug.Enable();
        }
        
        private void OnToggleDebug(InputAction.CallbackContext context) {
            if (!_console.ShowConsole) {
                _console.ShowConsole = true;
                ControlsManager.Instance.MainControl.Player.Disable();
            }
        }
        
        private void OnExitDebug(InputAction.CallbackContext context) {
            if (_console.ShowConsole) {
                HandleEscape();
                ControlsManager.Instance.MainControl.Player.Enable();
            }
        }

        private void OnReturn(InputAction.CallbackContext context) {
            OnReturn();
        }
        
        private void OnPrevCommand(InputAction.CallbackContext context) {
            _input = _console.GetBufferCommand(true);
            GUI.FocusControl("inputField");
        }
        
        private void OnPrevCommandBack(InputAction.CallbackContext context) {
            _input = _console.GetBufferCommand(false);
            GUI.FocusControl("inputField");
        }
    }
}