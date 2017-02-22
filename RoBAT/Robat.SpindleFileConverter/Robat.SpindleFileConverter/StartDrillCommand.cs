namespace Robat.SpindleFileConverter
{
    public sealed class StartDrillCommand : ICommand
    {
        public StartDrillCommand(string commandText)
        {
            CommandText = commandText;
        }

        public string CommandText { get; }

        public ICommand Translate()
        {
            // TODO
            return this;
        }

        public static ICommand FromCommandText(string commandText)
        {
            // TODO: Create properly
            return new StartDrillCommand(commandText);
        }
    }
}