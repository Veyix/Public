using Galleria.Support;
using System;

namespace Galleria.Profiles.Api.Client.CommandHandling
{
    /// <summary>
    /// A class that handles the "get" command.
    /// </summary>
    public sealed class GetCommandHandler : PrintUserProfileCommandHandler
    {
        private readonly IUserProfileService _userProfileService;

        public GetCommandHandler(IUserProfileService userProfileService)
        {
            Verify.NotNull(userProfileService, nameof(userProfileService));

            _userProfileService = userProfileService;
        }

        protected override void InvokeCore(InputCommand command)
        {
            string userIdString = command.GetParameterValue("userId");
            string companyIdString = command.GetParameterValue("companyId");

            if (!String.IsNullOrWhiteSpace(userIdString))
            {
                int userId;
                if (Int32.TryParse(userIdString, out userId))
                {
                    GetByUserId(userId);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{userIdString} is not a valid value for the 'userId' parameter");
                }
            }
            else if (!String.IsNullOrWhiteSpace(companyIdString))
            {
                int companyId;
                if (Int32.TryParse(companyIdString, out companyId))
                {
                    GetByCompanyId(companyId);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{companyIdString} is not a valid value for the 'companyId' parameter");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You must specify either the 'userId' or 'companyId' parameter for the get command");
            }
        }

        private void GetByCompanyId(int companyId)
        {
            var profiles = _userProfileService.GetUserProfilesByCompanyId(companyId);
            foreach (var profile in profiles)
            {
                PrintUserProfile(profile);
            }
        }

        private void GetByUserId(int userId)
        {
            var profile = _userProfileService.GetUserProfile(userId);
            PrintUserProfile(profile);
        }
    }
}