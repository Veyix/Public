using System.Collections.Generic;

namespace Robat.SpindleFileConverter
{
    public class CommandCollectionProcessor
    {
        private readonly List<ICommand> _commands;

        public CommandCollectionProcessor(IEnumerable<ICommand> commands)
        {
            Verify.NotNull(commands, nameof(commands));

            _commands = new List<ICommand>(commands);
        }

        public void Process()
        {
            // TODO: Fix commands - error detection / correction.
            // TODO: Optimize commands.
        }

        public ICommand[] GetCommands()
        {
            return _commands.ToArray();
        }
    }
}