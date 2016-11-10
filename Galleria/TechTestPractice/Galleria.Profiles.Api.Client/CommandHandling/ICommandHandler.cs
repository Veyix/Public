namespace Galleria.Profiles.Api.Client.CommandHandling
{
    /// <summary>
    /// Provides a contract for a command handler.
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// Invokes the command handler logic.
        /// </summary>
        /// <param name="command">The command to be handled.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="command"/> is null.</exception>
        void Invoke(InputCommand command);
    }
}