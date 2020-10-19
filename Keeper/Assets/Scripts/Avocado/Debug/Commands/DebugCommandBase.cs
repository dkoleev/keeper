using System;

namespace Avocado.Debug.Commands {
    public abstract class DebugCommandBase {
        public string CommandId { get; }
        public string CommandDescription { get; }
        public string CommandFormat { get; }


        protected DebugCommandBase(string commandId, string commandDescription, string commandFormat) {
            CommandId = commandId;
            CommandDescription = commandDescription;
            CommandFormat = commandFormat;
        }
    }
}