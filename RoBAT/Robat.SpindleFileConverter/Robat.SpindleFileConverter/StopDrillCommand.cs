namespace Robat.SpindleFileConverter
{
    public sealed class StopDrillCommand : ICommand
    {
        public StopDrillCommand(string commandText)
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
            return new StopDrillCommand(commandText);
        }
    }
}