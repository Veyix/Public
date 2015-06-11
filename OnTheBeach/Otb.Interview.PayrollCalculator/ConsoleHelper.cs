using System;

namespace Otb.Interview.PayrollCalculator
{
    /// <summary>
    /// A class that provides support for working with the console.
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Writes the given message to the console in red text.
        /// </summary>
        /// <param name="message">The message to be written to the console.</param>
        /// <param name="arguments">An optional set of arguments for message formatting.</param>
        public static void WriteError(string message, params object[] arguments)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message, arguments);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes the given message to the console in cyan text.
        /// </summary>
        /// <param name="message">The message to be written to the console.</param>
        /// <param name="arguments">An optional set of arguments for message formatting.</param>
        public static void WriteInformation(string message, params object[] arguments)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(message, arguments);
            Console.ResetColor();
        }
    }
}