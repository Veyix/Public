using Galleria.Profiles.ObjectModel;
using Galleria.Support;
using System;
using System.Globalization;

namespace Galleria.Profiles.Api.Client.CommandHandling
{
    /// <summary>
    /// A class that handles the "create" command.
    /// </summary>
    public sealed class CreateCommandHandler : CommandHandler
    {
        private readonly IUserProfileService _userProfileService;

        public CreateCommandHandler(IUserProfileService userProfileService)
        {
            Verify.NotNull(userProfileService, nameof(userProfileService));

            _userProfileService = userProfileService;
        }

        protected override void InvokeCore(InputCommand command)
        {
            var profile = new UserProfile();

            // Extract all of the parameter values
            string companyIdValue = command.GetParameterValue("CompanyId");
            if (!String.IsNullOrWhiteSpace(companyIdValue))
            {
                profile.CompanyId = Convert.ToInt32(companyIdValue);
            }

            profile.Title = command.GetParameterValue("Title");
            profile.Forename = command.GetParameterValue("Forename");
            profile.Surname = command.GetParameterValue("Surname");

            string dateOfBirthValue = command.GetParameterValue("DateOfBirth");
            string dateFormat = command.GetParameterValue("DateFormat") ?? "yyyy-MM-dd";

            if (!String.IsNullOrWhiteSpace(dateOfBirthValue))
            {
                profile.DateOfBirth = DateTime.ParseExact(dateOfBirthValue, dateFormat, CultureInfo.InvariantCulture);
            }

            _userProfileService.CreateUserProfile(profile);
        }
    }
}