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
            // TODO: Fix commands - error detection / correction.

            ProcessCommands();
            OptimiseCommands();
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
            }
            else
            {
                _processedCommands.Add(command);
            }
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
            // TODO: Remove commands if they are not needed.
        }

        #endregion

        public ICommand[] GetCommands()
        {
            return _processedCommands.ToArray();
        }
    }
}