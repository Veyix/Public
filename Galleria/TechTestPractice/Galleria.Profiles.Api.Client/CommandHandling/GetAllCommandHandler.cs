using Galleria.Support;

namespace Galleria.Profiles.Api.Client.CommandHandling
{
    /// <summary>
    /// A class that handles the "getall" command.
    /// </summary>
    public sealed class GetAllCommandHandler : PrintUserProfileCommandHandler
    {
        private readonly IUserProfileService _userProfileService;

        public GetAllCommandHandler(IUserProfileService userProfileService)
        {
            Verify.NotNull(userProfileService, nameof(userProfileService));

            _userProfileService = userProfileService;
        }

        protected override void InvokeCore(InputCommand command)
        {
            var profiles = _userProfileService.GetUserProfiles();
            foreach (var profile in profiles)
            {
                PrintUserProfile(profile);
            }
        }
    }
}