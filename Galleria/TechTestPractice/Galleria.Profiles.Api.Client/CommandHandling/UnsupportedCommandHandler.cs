using System;

namespace Galleria.Profiles.Api.Client.CommandHandling
{
    /// <summary>
    /// A class that handles an unsupported command.
    /// </summary>
    public sealed class UnsupportedCommandHandler : CommandHandler
    {
        protected override void InvokeCore(InputCommand command)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"'{command.CommandText}' is not a recognised command");
            Console.ResetColor();
        }
    }
}