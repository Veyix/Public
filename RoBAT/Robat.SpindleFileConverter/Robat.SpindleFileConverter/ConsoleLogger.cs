using System;

namespace Robat.SpindleFileConverter
{
    public static class ConsoleLogger
    {
        public static void WriteError(string message)
        {
            Verify.NotNullOrEmpty(message, nameof(message));

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void WriteError(Exception exception)
        {
            Verify.NotNull(exception, nameof(exception));

            WriteError(exception.Message);
        }

        public static void WriteInformation(string message)
        {
            Verify.NotNullOrEmpty(message, nameof(message));

            Console.WriteLine(message);
        }
    }
}