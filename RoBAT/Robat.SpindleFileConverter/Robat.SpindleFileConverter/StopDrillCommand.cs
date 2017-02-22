using System;
using System.Text.RegularExpressions;

namespace Robat.SpindleFileConverter
{
    public sealed class StopDrillCommand : ICommand
    {
        private readonly int? _headNumber;

        public StopDrillCommand(string commandText, int? headNumber)
        {
            CommandText = commandText;

            _headNumber = headNumber;
        }

        public string CommandText { get; }

        public ICommand Translate()
        {
            // TODO
            return this;
        }

        public static ICommand FromCommandText(string commandText)
        {
            int? headNumber = GetHeadNumber(commandText);
            return new StopDrillCommand(commandText, headNumber);
        }

        private static int? GetHeadNumber(string commandText)
        {
            const string pattern = @"^M05(\sH([0-9]))*$";
            var match = Regex.Match(commandText, pattern);

            if (match.Groups.Count < 3)
            {
                // No head number specified
                return null;
            }

            string headNumberString = match.Groups[2].Value;
            int headNumber;

            if (!Int32.TryParse(headNumberString, out headNumber))
            {
                throw new FormatException($"The head number '{headNumberString}' is not a valid value");
            }

            return headNumber;
        }
    }
}