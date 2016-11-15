using Galleria.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galleria.Profiles.Api.Client
{
    /// <summary>
    /// A class that represents a command given through an input mechanism.
    /// </summary>
    public sealed class InputCommand
    {
        private const string COMMAND_EXIT = "exit";

        private readonly Dictionary<string, string> _parameters =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        private InputCommand(string commandText, bool isExitCommand)
        {
            CommandText = commandText;
            IsExitCommand = isExitCommand;
        }

        /// <summary>
        /// Gets the text given for the command.
        /// </summary>
        public string CommandText { get; }

        /// <summary>
        /// Gets a value that indicates whether the command is to exit the application.
        /// </summary>
        public bool IsExitCommand { get; }

        private void AddParameter(string parameterName, string parameterValue)
        {
            _parameters.Add(parameterName, parameterValue);
        }

        /// <summary>
        /// Gets the value of the specified parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter for which to get the value.</param>
        /// <returns>The value of the parameter; or null if the parameter does not exist.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="parameterName"/> is a null or empty string.</exception>
        public string GetParameterValue(string parameterName)
        {
            Verify.NotNullOrEmpty(parameterName, nameof(parameterName));

            string parameterValue;
            if (!_parameters.TryGetValue(parameterName, out parameterValue))
            {
                return null;
            }

            return parameterValue;
        }

        /// <summary>
        /// Creates an input command from the given text.
        /// </summary>
        /// <param name="inputText">The text from which to create the command.</param>
        /// <returns>A new instance of the <see cref="InputCommand"/> class.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="inputText"/> is null or empty.</exception>
        public static InputCommand Create(string inputText)
        {
            Verify.NotNullOrEmpty(inputText, nameof(inputText));

            string[] components = GetComponents(inputText);

            string commandText = components[0];
            bool isExitCommand = String.Equals(COMMAND_EXIT, commandText, StringComparison.InvariantCultureIgnoreCase);

            var command = new InputCommand(commandText, isExitCommand);

            // Parse all of the parameters from the components
            for (int i = 1; i < components.Length; i++)
            {
                string component = components[i];
                int separatorIndex = component.IndexOf('=');

                string parameterName = component.Substring(0, separatorIndex);
                string parameterValue = component.Substring(separatorIndex + 1);

                command.AddParameter(parameterName, parameterValue);
            }

            return command;
        }

        private static string SanitizeInputText(string inputText)
        {
            return inputText.Trim(' ').ToLowerInvariant();
        }

        private static string[] GetComponents(string inputText)
        {
            var components = new List<string>();
            var componentBuilder = new StringBuilder();

            foreach (char character in inputText)
            {
                if (Char.IsWhiteSpace(character) && componentBuilder.Length > 0)
                {
                    // We have our first whitespace character, so add the contents of the
                    // builder to the component list and reset it until we find our next
                    // non-whitespace character
                    components.Add(componentBuilder.ToString());
                    componentBuilder.Clear();
                }
                else
                {
                    componentBuilder.Append(character);
                }
            }

            if (componentBuilder.Length > 0)
            {
                // We have content left in the builder, so add it as the final component
                components.Add(componentBuilder.ToString());
            }

            return components.ToArray();
        }
    }
}