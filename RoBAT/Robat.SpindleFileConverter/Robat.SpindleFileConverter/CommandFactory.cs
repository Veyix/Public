using System;
using System.Collections.Generic;

namespace Robat.SpindleFileConverter
{
    public sealed class CommandFactory
    {
        private readonly Dictionary<CommandType, Func<string, ICommand>> _commandCreationFunctions =
            new Dictionary<CommandType, Func<string, ICommand>>();

        public CommandFactory()
        {
            _commandCreationFunctions[CommandType.ToolSelection] = ToolSelectionCommand.FromCommandText;
            _commandCreationFunctions[CommandType.StartDrill] = StartDrillCommand.FromCommandText;
            _commandCreationFunctions[CommandType.StopDrill] = StopDrillCommand.FromCommandText;
            _commandCreationFunctions[CommandType.DrillHole] = DrillHoleCommand.FromCommandText;
        }

        public ICommand CreateCommand(string commandText)
        {
            var function = GetCommandCreationFunction(commandText);
            return function.Invoke(commandText);
        }

        private Func<string, ICommand> GetCommandCreationFunction(string commandText)
        {
            var commandType = GetCommandType(commandText);
            Func<string, ICommand> commandCreationFunction;

            if (!_commandCreationFunctions.TryGetValue(commandType, out commandCreationFunction))
            {
                commandCreationFunction = NullCommand.FromCommandText;
            }

            return commandCreationFunction;
        }

        private static CommandType GetCommandType(string commandText)
        {
            if (String.IsNullOrWhiteSpace(commandText))
            {
                return CommandType.EmptyLine;
            }

            if (commandText.StartsWith("%"))
            {
                return CommandType.Comment;
            }

            if (commandText.StartsWith("T"))
            {
                return CommandType.ToolSelection;
            }

            if (commandText.StartsWith("M03 "))
            {
                return CommandType.StartDrill;
            }

            if (commandText.StartsWith("M05 "))
            {
                return CommandType.StopDrill;
            }

            if (commandText.StartsWith("G73 "))
            {
                return CommandType.DrillHole;
            }

            // TODO: What if the command starts with the right type but is of the wrong format?

            return CommandType.Unknown;
        }

        private enum CommandType
        {
            Unknown,
            EmptyLine,
            Comment,
            ToolSelection,
            StartDrill,
            StopDrill,
            DrillHole
        }
    }
}