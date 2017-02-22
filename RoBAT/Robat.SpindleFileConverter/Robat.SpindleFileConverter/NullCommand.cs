namespace Robat.SpindleFileConverter
{
    public sealed class NullCommand : ICommand
    {
        public NullCommand(string commandText)
        {
            CommandText = commandText;
        }

        /// <summary>
        /// Gets or sets the value that identifies this command.
        /// </summary>
        public int CommandId { get; set; }

        public string CommandText { get; }

        public ICommand Translate()
        {
            // This command cannot be translated
            return this;
        }

        public static ICommand FromCommandText(string commandText)
        {
            return new NullCommand(commandText);
        }
    }
}