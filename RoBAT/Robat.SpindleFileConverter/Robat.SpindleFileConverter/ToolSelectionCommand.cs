using System;
using System.Text.RegularExpressions;

namespace Robat.SpindleFileConverter
{
    public sealed class ToolSelectionCommand : ICommand
    {
        private readonly int _toolNumber;
        private readonly int? _headNumber;

        public ToolSelectionCommand(string commandText, int toolNumber, int? headNumber)
        {
            CommandText = commandText;

            _toolNumber = toolNumber;
            _headNumber = headNumber;
        }

        /// <summary>
        /// Gets or sets the value that identifies this command.
        /// </summary>
        public int CommandId { get; set; }

        public string CommandText { get; }

        public ICommand Translate()
        {
            // TODO
            return this;
        }

        public static ICommand FromCommandText(string commandText)
        {
            int toolNumber = GetToolNumber(commandText);
            int? headNumber = GetHeadNumber(commandText);

            return new ToolSelectionCommand(commandText, toolNumber, headNumber);
        }

        private static int GetToolNumber(string commandText)
        {
            const string pattern = @"^T([0-9])";
            var match = Regex.Match(commandText, pattern);

            if (match.Groups.Count < 2)
            {
                throw new FormatException($"The command '{commandText}' does not contain a valid tool number");
            }

            int toolNumber;
            if (!Int32.TryParse(match.Groups[1].Value, out toolNumber))
            {
                throw new FormatException($"The command '{commandText}' does not contain a valid tool number");
            }

            return toolNumber;
        }

        private static int? GetHeadNumber(string commandText)
        {
            const string pattern = @"^T[0-9]\sH([0-9])$";
            var match = Regex.Match(commandText, pattern);

            if (match.Groups.Count < 2)
            {
                // No head number specified
                return null;
            }

            string headNumberString = match.Groups[1].Value;

            int headNumber;
            if (!Int32.TryParse(headNumberString, out headNumber))
            {
                throw new FormatException($"The head number '{headNumberString}' is not a valid head number");
            }

            return headNumber;
        }
    }
}