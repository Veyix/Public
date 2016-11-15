using Galleria.Profiles.Api.Client.CommandHandling;
using Galleria.Support;

namespace Galleria.Profiles.Api.Client
{
    /// <summary>
    /// A class that manages the flow of the client application.
    /// </summary>
    public sealed class ClientApplication
    {
        private readonly IInputReceiver _inputReceiver;
        private readonly CommandHandlerFactory _commandHandlerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientApplication"/> class.
        /// </summary>
        /// <param name="inputReceiver">A receiver of input commands.</param>
        /// <param name="userProfileService">A service that provides access to the user profile API.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the given parameters are null.</exception>
        public ClientApplication(IInputReceiver inputReceiver, IUserProfileService userProfileService)
        {
            Verify.NotNull(inputReceiver, nameof(inputReceiver));
            Verify.NotNull(userProfileService, nameof(userProfileService));

            _inputReceiver = inputReceiver;
            _commandHandlerFactory = new CommandHandlerFactory(userProfileService);
        }

        /// <summary>
        /// Runs the logic of the application.
        /// </summary>
        public void Run()
        {
            InputCommand command;

            do
            {
                command = _inputReceiver.GetNextInputCommand();

                if (!command.IsExitCommand)
                {
                    var handler = _commandHandlerFactory.Create(command);
                    handler.Invoke(command);
                }
            }
            while (command != null && !command.IsExitCommand);
        }
    }
}