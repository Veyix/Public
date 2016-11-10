namespace Galleria.Profiles.Api.Client
{
    /// <summary>
    /// Provides a contract for a receiver of input commands.
    /// </summary>
    public interface IInputReceiver
    {
        /// <summary>
        /// Retrieves the next input command.
        /// </summary>
        /// <returns>The next input command to execute within the application.</returns>
        InputCommand GetNextInputCommand();
    }
}