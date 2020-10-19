using System;

namespace Avocado.Debug.Commands {
    public class DebugCommand : DebugCommandBase {
        public Action _command;
        
        public DebugCommand(string commandId, string commandDescription, string commandFormat, Action command) : base(commandId,
            commandDescription, commandFormat) {
            _command = command;
        }

        public void Invoke() {
            _command.Invoke();
        }
    }
    
    public class DebugCommand<T1> : DebugCommandBase {
        public Action<T1> _command;
        
        public DebugCommand(string commandId, string commandDescription, string commandFormat, Action<T1> command) : base(commandId,
            commandDescription, commandFormat) {
            _command = command;
        }

        public void Invoke(T1 parameter) {
            _command.Invoke(parameter);
        }
    }
}