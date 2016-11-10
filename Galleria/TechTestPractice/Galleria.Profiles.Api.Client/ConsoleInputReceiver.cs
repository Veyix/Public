using System;

namespace Galleria.Profiles.Api.Client
{
    /// <summary>
    /// A class that receives input commands from user interaction with the console window.
    /// </summary>
    public sealed class ConsoleInputReceiver : IInputReceiver
    {
        public InputCommand GetNextInputCommand()
        {
            Console.Write("Enter command: ");

            string inputText = Console.ReadLine();
            return InputCommand.Create(inputText);
        }
    }
}