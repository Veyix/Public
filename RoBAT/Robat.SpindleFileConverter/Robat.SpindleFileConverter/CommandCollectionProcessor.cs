using System.Collections.Generic;
using System.Linq;

namespace Robat.SpindleFileConverter
{
    public class CommandCollectionProcessor
    {
        private readonly List<ICommand> _commands;
        private readonly List<ICommand> _processedCommands = new List<ICommand>();

        public CommandCollectionProcessor(IEnumerable<ICommand> commands)
        {
            Verify.NotNull(commands, nameof(commands));

            _commands = new List<ICommand>(commands);
        }

        public void Process()
        {
            ProcessCommands();
            OptimiseCommands();
            InsertMissingDrillStarts();
            RemoveRedundantCommands();
        }

        #region Command Processing

        private void ProcessCommands()
        {
            int currentCommandIndex = 0;
            ICommand command = null;

            do
            {
                command = GetNextCommand(currentCommandIndex);

                if (command != null)
                {
                    Process(currentCommandIndex, command);
                }

                currentCommandIndex++;
            }
            while (command != null);
        }

        private void Process(int commandIndex, ICommand command)
        {
            var drillCommand = command as DrillHoleCommand;
            if (drillCommand != null)
            {
                ProcessDrillHoleCommand(commandIndex, drillCommand);

                return;
            }

            var toolCommand = command as ToolSelectionCommand;
            if (toolCommand != null)
            {
                ProcessToolSelectionCommand(commandIndex, toolCommand);

                return;
            }

            _processedCommands.Add(command);
        }

        private void ProcessDrillHoleCommand(int commandIndex, DrillHoleCommand command)
        {
            double xCoordinate = GetXCoordinate(command, commandIndex);
            double yCoordinate = GetYCoordinate(command, commandIndex);
            double zCoordinate = command.ZCoordinate;

            if (xCoordinate > 500d)
            {
                xCoordinate -= 500d;

                // We need to replace this command with a drill command for the second head
                // Note: Make sure we start and stop the drill before and after. This will be
                //       optimised out later on if it's not needed.
                var startDrillCommand = StartDrillCommand.FromHeadNumber(2);
                var drillHoleCommand = DrillHoleCommand.FromCoordinates(xCoordinate, yCoordinate, zCoordinate);
                var stopDrillCommand = StopDrillCommand.FromHeadNumber(2);

                _processedCommands.Add(startDrillCommand);
                _processedCommands.Add(drillHoleCommand);
                _processedCommands.Add(stopDrillCommand);
            }
            else
            {
                _processedCommands.Add(command);
            }
        }

        private void ProcessToolSelectionCommand(int commandIndex, ToolSelectionCommand command)
        {
            // Create a new tool selection command but don't specify a head number so that both heads are changed
            var toolCommand = ToolSelectionCommand.FromNumbers(command.ToolNumber);
            _processedCommands.Add(toolCommand);
        }

        private double GetXCoordinate(DrillHoleCommand command, int commandIndex)
        {
            double xCoordinate = command.XCoordinate ?? 0d;

            if (!command.XCoordinate.HasValue)
            {
                // Find the last known x-coordinate, if any
                var previousDrillCommand = _commands.Take(commandIndex)
                    .Where(x => x is DrillHoleCommand)
                    .Cast<DrillHoleCommand>()
                    .Where(x => x.XCoordinate.HasValue)
                    .LastOrDefault();

                xCoordinate = previousDrillCommand?.XCoordinate ?? 0d;
            }

            return xCoordinate;
        }

        private double GetYCoordinate(DrillHoleCommand command, int commandIndex)
        {
            double yCoordinate = command.YCoordinate ?? 0d;

            if (!command.YCoordinate.HasValue)
            {
                // Find the last known y-coordinate, if any
                var previousDrillCommand = _commands.Take(commandIndex)
                    .Where(x => x is DrillHoleCommand)
                    .Cast<DrillHoleCommand>()
                    .Where(x => x.YCoordinate.HasValue)
                    .LastOrDefault();

                yCoordinate = previousDrillCommand?.YCoordinate ?? 0d;
            }

            return yCoordinate;
        }

        private ICommand GetNextCommand(int currentCommandIndex)
        {
            if (currentCommandIndex >= _commands.Count)
            {
                return null;
            }

            return _commands[currentCommandIndex];
        }

        #endregion

        #region Command Optimisation

        private void OptimiseCommands()
        {
            // Get a copy of the commands
            var commands = _processedCommands.ToArray();

            for (int currentIndex = 0; currentIndex < commands.Length - 1; currentIndex++)
            {
                var command = commands[currentIndex];

                var stopDrillCommand = command as StopDrillCommand;
                if (stopDrillCommand != null)
                {
                    CheckForStartCommand(commands, stopDrillCommand, currentIndex);

                    continue;
                }

                var startDrillCommand = command as StartDrillCommand;
                if (startDrillCommand != null)
                {
                    CheckOtherDrillIsStopped(commands, startDrillCommand, currentIndex);
                }
            }
        }

        private void CheckForStartCommand(ICommand[] commands, StopDrillCommand command, int currentIndex)
        {
            // See if we can find a start drill command before a tool change
            var nextCommands = commands.Skip(currentIndex + 1).ToArray();
            foreach (var nextCommand in nextCommands)
            {
                if (nextCommand is ToolSelectionCommand)
                {
                    // We are changing the tool, so it's OK to stop the drill
                    break;
                }

                var startDrillCommand = nextCommand as StartDrillCommand;
                if (startDrillCommand != null && startDrillCommand.HeadNumber == command.HeadNumber)
                {
                    // We don't need to stop and re-start the drill, so remove these commands
                    _processedCommands.Remove(command);
                    _processedCommands.Remove(nextCommand);
                }
            }
        }

        private void CheckOtherDrillIsStopped(ICommand[] commands, StartDrillCommand command, int currentIndex)
        {
            // Look at the previous commands and make sure we find a stop command for the other drill before a start command
            var previousCommands = commands.Take(currentIndex).Reverse().ToArray();
            foreach (var previousCommand in previousCommands)
            {
                var stopDrillCommand = previousCommand as StopDrillCommand;
                if (stopDrillCommand != null && stopDrillCommand.HeadNumber != command.HeadNumber)
                {
                    // The other drill has been stopped before this one has been started
                    break;
                }

                var startDrillCommand = previousCommand as StartDrillCommand;
                if (startDrillCommand != null && startDrillCommand.HeadNumber != command.HeadNumber)
                {
                    // The other drill was started but not stopped before this drill was started,
                    // so create a new command to stop the other drill
                    var stopOtherDrillCommand = StopDrillCommand.FromHeadNumber(startDrillCommand.HeadNumber);
                    int newCommandIndex = _processedCommands.IndexOf(command);

                    if (newCommandIndex > -1)
                    {
                        // Note: If we don't find the index it's because the command has already been removed.
                        _processedCommands.Insert(newCommandIndex, stopOtherDrillCommand);
                    }
                }
            }
        }

        #endregion

        #region Error Checking

        private void InsertMissingDrillStarts()
        {
            bool drill1Started = false;
            bool drill2Started = false;

            for (int index = 0; index < _processedCommands.Count; index++)
            {
                var command = _processedCommands[index];

                if (command is StartDrillCommand)
                {
                    var startDrillCommand = (StartDrillCommand)command;
                    if (startDrillCommand.HeadNumber == 1)
                    {
                        drill1Started = true;
                    }
                    else
                    {
                        drill2Started = true;
                    }
                }
                else if (command is StopDrillCommand)
                {
                    var stopDrillCommand = (StopDrillCommand)command;
                    if (stopDrillCommand.HeadNumber == 1)
                    {
                        drill1Started = false;
                    }
                    else
                    {
                        drill2Started = false;
                    }
                }
                else if (command is DrillHoleCommand)
                {
                    var drillHoleCommand = (DrillHoleCommand)command;
                    if (!drill1Started && !drill2Started && drillHoleCommand.XCoordinate.HasValue)
                    {
                        double xCoordinate = drillHoleCommand.XCoordinate.Value;
                        int headNumber = xCoordinate <= 500 ? 1 : 2;

                        var startDrillCommand = StartDrillCommand.FromHeadNumber(headNumber);
                        _processedCommands.Insert(index, startDrillCommand);

                        if (headNumber == 1)
                        {
                            drill1Started = true;
                        }
                        else
                        {
                            drill2Started = true;
                        }
                    }
                }
            }
        }

        private void RemoveRedundantCommands()
        {
            bool drill1Started = false;
            bool drill2Started = false;

            var commandsToRemove = new List<ICommand>();
            foreach (var command in _processedCommands)
            {
                var startDrillCommand = command as StartDrillCommand;
                if (startDrillCommand != null)
                {
                    if (startDrillCommand.HeadNumber == 1)
                    {
                        if (drill1Started)
                        {
                            // The drill has already been started, so remove this command
                            commandsToRemove.Add(command);
                        }
                        else
                        {
                            drill1Started = true;
                        }
                    }
                    else if (startDrillCommand.HeadNumber == 2)
                    {
                        if (drill2Started)
                        {
                            // The drill has already been started, so remove this command
                            commandsToRemove.Add(command);
                        }
                        else
                        {
                            drill2Started = true;
                        }
                    }

                    continue;
                }

                var stopDrillCommand = command as StopDrillCommand;
                if (stopDrillCommand != null)
                {
                    if (stopDrillCommand.HeadNumber == 1)
                    {
                        if (drill1Started)
                        {
                            drill1Started = false;
                        }
                        else
                        {
                            // The drill has already been stopped, so remove this command
                            commandsToRemove.Add(command);
                        }
                    }
                    else if (stopDrillCommand.HeadNumber == 2)
                    {
                        if (drill2Started)
                        {
                            drill2Started = false;
                        }
                        else
                        {
                            // The drill has already been stopped, so remove this command
                            commandsToRemove.Add(command);
                        }
                    }
                }
            }

            foreach (var command in commandsToRemove)
            {
                _processedCommands.Remove(command);
            }
        }

        #endregion

        public ICommand[] GetCommands()
        {
            return _processedCommands.ToArray();
        }
    }
}