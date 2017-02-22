namespace Robat.SpindleFileConverter
{
    public sealed class NullCommand : ICommand
    {
        public NullCommand(string commandText)
        {
            CommandText = commandText;
        }

        public string CommandText { get; }

        public ICommand Translate()
        {
            // This command cannot be translated
            return this;
        }
    }
}