using Galleria.Support;
using System;

namespace Galleria.Profiles.Api.Client.CommandHandling
{
    /// <summary>
    /// A class that handles the "login" command.
    /// </summary>
    public sealed class LoginCommandHandler : CommandHandler
    {
        private readonly IUserProfileService _userProfileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginCommandHandler"/> class.
        /// </summary>
        /// <param name="userProfileService">A service that provides access to the user profile API.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="userProfileService"/> is null.</exception>
        public LoginCommandHandler(IUserProfileService userProfileService)
        {
            Verify.NotNull(userProfileService, nameof(userProfileService));

            _userProfileService = userProfileService;
        }

        protected override void InvokeCore(InputCommand command)
        {
            string username = command.GetParameterValue("username");
            string password = command.GetParameterValue("password");

            if (String.IsNullOrWhiteSpace(username))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Missing 'username' parameter");
            }
            else if (String.IsNullOrWhiteSpace(password))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Missing 'password' parameter");
            }
            else
            {
                _userProfileService.Login(username, password);
            }

            Console.ResetColor();
        }
    }
}