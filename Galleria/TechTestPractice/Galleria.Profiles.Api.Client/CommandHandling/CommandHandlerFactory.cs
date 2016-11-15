using System;

namespace Galleria.Profiles.Api.Client.CommandHandling
{
    /// <summary>
    /// A class that creates command handler instances.
    /// </summary>
    public sealed class CommandHandlerFactory
    {
        private readonly IUserProfileService _userProfileService;

        public CommandHandlerFactory(IUserProfileService userProfileService)
        {
            if (userProfileService == null) throw new ArgumentNullException(nameof(userProfileService));

            _userProfileService = userProfileService;
        }

        /// <summary>
        /// Creates a handler for the given input command.
        /// </summary>
        /// <param name="command">The command for which to create a handler.</param>
        /// <returns>An appropriate implementation of the <see cref="ICommandHandler"/> interface.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="command"/> is null.</exception>
        public ICommandHandler Create(InputCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            switch (command.CommandText)
            {
                case "login":
                    return new LoginCommandHandler(_userProfileService);

                case "getall":
                    return new GetAllCommandHandler(_userProfileService);

                case "get":
                    return new GetCommandHandler(_userProfileService);

                case "create":
                    return new CreateCommandHandler(_userProfileService);

                case "update":
                    return new UpdateCommandHandler(_userProfileService);

                case "delete":
                    return new DeleteCommandHandler(_userProfileService);

                default:
                    return new UnsupportedCommandHandler();
            }
        }
    }
}