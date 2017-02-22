using System;
using System.Text.RegularExpressions;

namespace Robat.SpindleFileConverter
{
    public sealed class DrillHoleCommand : ICommand
    {
        private readonly double? _xCoordinate;
        private readonly double? _yCoordinate;
        private readonly double _zCoordinate;

        public DrillHoleCommand(string commandText, double? xCoordinate, double? yCoordinate, double zCoordinate)
        {
            CommandText = commandText;

            _xCoordinate = xCoordinate;
            _yCoordinate = yCoordinate;
            _zCoordinate = zCoordinate;
        }

        public string CommandText { get; }

        public ICommand Translate()
        {
            // TODO
            return this;
        }

        public static ICommand FromCommandText(string commandText)
        {
            double? xCoordinate = GetXCoordinate(commandText);
            double? yCoordinate = GetYCoordinate(commandText);
            double zCoordinate = GetZCoordinate(commandText);

            return new DrillHoleCommand(commandText, xCoordinate, yCoordinate, zCoordinate);
        }

        private static double? GetXCoordinate(string commandText)
        {
            const string pattern = @"\sX(\d+\.?\d*)\s";
            var match = Regex.Match(commandText, pattern);

            if (match.Groups.Count < 2)
            {
                // No x-coordinate specified
                return null;
            }

            string xCoordinateString = match.Groups[1].Value;
            double xCoordinate;

            if (!Double.TryParse(xCoordinateString, out xCoordinate))
            {
                throw new FormatException($"The value '{xCoordinateString}' is not valid for the x-coordinate");
            }

            return xCoordinate;
        }

        private static double? GetYCoordinate(string commandText)
        {
            const string pattern = @"\sY(\d+\.?\d*)\s";
            var match = Regex.Match(commandText, pattern);

            if (match.Groups.Count < 2)
            {
                // No y-coordinate specified
                return null;
            }

            string yCoordinateString = match.Groups[1].Value;
            double yCoordinate;

            if (!Double.TryParse(yCoordinateString, out yCoordinate))
            {
                throw new FormatException($"The value '{yCoordinateString}' is not valid for the y-coordinate");
            }

            return yCoordinate;
        }

        private static double GetZCoordinate(string commandText)
        {
            const string pattern = @"\sZ(\d+\.?\d*)$";
            var match = Regex.Match(commandText, pattern);

            if (match.Groups.Count < 2)
            {
                throw new FormatException($"The command '{commandText}' does not specified a z-coordinate");
            }

            string zCoordinateString = match.Groups[1].Value;
            double zCoordinate;

            if (!Double.TryParse(zCoordinateString, out zCoordinate))
            {
                throw new FormatException($"The value '{zCoordinateString}' is not valid for the z-coordinate");
            }

            return zCoordinate;
        }
    }
}