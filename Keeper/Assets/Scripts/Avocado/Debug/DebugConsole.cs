using System.Collections.Generic;
using Avocado.Core.Controls;
using Avocado.Debug.Commands;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Avocado.Debug {
    public class DebugConsole : MonoBehaviour {
        private static DebugCommandBase HelpCommand;
        public static DebugCommandBase KillAll;
        public static DebugCommandBase AddGold;
        
        private List<DebugCommandBase> _commands;

        private bool _showConsole;
        private bool _showHelp;
        private string _input;

        private void Awake() {
            ControlsManager.Instance.MainControl.Debug.ToggleDebug.performed += OnToggleDebug;
            ControlsManager.Instance.MainControl.Debug.Return.performed += OnReturn;
            ControlsManager.Instance.MainControl.Debug.Enable();
            
            HelpCommand = new DebugCommand("help", "show a list of commands", "help", () => {
                _showHelp = true;
            });

            KillAll = new DebugCommand("kill_all", "kill all enemies", "kill_all", () => {
                UnityEngine.Debug.LogError("Kill all");
            });
            
            AddGold = new DebugCommand<int>("add_gold", $"add gold <amount>", "add_gold", (amount) => {
                UnityEngine.Debug.LogError("add gold " + amount);
            });
            
            _commands = new List<DebugCommandBase> {
                HelpCommand,
                KillAll,
                AddGold
            };
        }

        public void OnToggleDebug(InputAction.CallbackContext context) {
            _showConsole = !_showConsole;
            if (_showConsole) {
                ControlsManager.Instance.MainControl.Player.Disable();
            } else {
                ControlsManager.Instance.MainControl.Player.Enable();
            }
        }

        public void OnReturn(InputAction.CallbackContext context) {
            if (_showConsole) {
                HandleInput();
                _input = "";
            }
        }

        private Vector2 _scroll;
        private void OnGUI() {
            if (!_showConsole) {
                return;
            }

            var inputHeight = 100;
            var y = Screen.height - inputHeight;

            if (_showHelp) {
                GUI.Box(new Rect(0, y - 400, Screen.width, 400), "");
                var viewPort = new Rect(0, 0, Screen.width - 30, 80 * _commands.Count);
                _scroll = GUI.BeginScrollView(new Rect(0, y - 380f, Screen.width, 380), _scroll, viewPort);
             
                for (int i = 0; i < _commands.Count; i++) {
                    var command = _commands[i];
                    var label = $"{command.CommandFormat} - {command.CommandDescription}";
                    var labelRect = new Rect(5, 60*i, viewPort.width-100, 60);
                    
                    var labelStyleHelp = new GUIStyle("label");
                    labelStyleHelp.fontStyle = FontStyle.Normal;
                    labelStyleHelp.fontSize = 40;
                    
                    GUI.Label(labelRect, label, labelStyleHelp);
                }
                
                GUI.EndScrollView();
            }


            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            GUI.backgroundColor = Color.black;
            
            var labelStyle = new GUIStyle("TextField");
            labelStyle.fontStyle = FontStyle.Normal;
            labelStyle.fontSize = 60;
            _input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20, 80f), _input, labelStyle);
        }

        private void HandleInput() {
            var properties = _input.Split(' ');
            foreach (var command in _commands) {
                if (_input.Contains(command.CommandId)) {
                    if (command is DebugCommand debugCommand) {
                        debugCommand.Invoke();
                    }else if (command is DebugCommand<int> debugCommandParInt) {
                        debugCommandParInt.Invoke(int.Parse(properties[1]));
                    }
                }
            }
        }
    }
}