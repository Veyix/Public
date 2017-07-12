namespace Robat.SpindleFileConverter
{
    /// <summary>
    /// A contract for a command contained within the file.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets or sets the value that identifies this command.
        /// </summary>
        int CommandId { get; set; }

        /// <summary>
        /// Gets the text of the command.
        /// </summary>
        string CommandText { get; }

        /// <summary>
        /// Translates the single-spindle command into a double-spindle command.
        /// </summary>
        /// <returns>The translated command instance.</returns>
        ICommand Translate();
    }
}