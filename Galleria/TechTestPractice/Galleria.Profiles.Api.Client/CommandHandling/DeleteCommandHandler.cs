using Galleria.Support;
using System;

namespace Galleria.Profiles.Api.Client.CommandHandling
{
    /// <summary>
    /// A class that handles the "delete" command.
    /// </summary>
    public sealed class DeleteCommandHandler : CommandHandler
    {
        private readonly IUserProfileService _userProfileService;

        public DeleteCommandHandler(IUserProfileService userProfileService)
        {
            Verify.NotNull(userProfileService, nameof(userProfileService));

            _userProfileService = userProfileService;
        }

        protected override void InvokeCore(InputCommand command)
        {
            string userIdValue = command.GetParameterValue("UserId");

            int userId;
            if (String.IsNullOrWhiteSpace(userIdValue) || !Int32.TryParse(userIdValue, out userId))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("Missing 'userId' parameter");
                Console.ResetColor();

                return;
            }

            _userProfileService.DeleteUserProfile(userId);
        }
    }
}