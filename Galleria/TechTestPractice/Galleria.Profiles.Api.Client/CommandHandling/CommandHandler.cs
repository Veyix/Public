using System;

namespace Galleria.Profiles.Api.Client.CommandHandling
{
    /// <summary>
    /// A class that provides an abstract implementation of the <see cref="ICommandHandler"/> contract.
    /// </summary>
    public abstract class CommandHandler : ICommandHandler
    {
        public void Invoke(InputCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            InvokeCore(command);
        }

        protected abstract void InvokeCore(InputCommand command);
    }
}