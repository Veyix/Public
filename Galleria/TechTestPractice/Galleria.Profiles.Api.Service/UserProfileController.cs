using Galleria.Profiles.Infrastructure.AdoNet;
using Galleria.Profiles.ObjectModel;
using System.Collections.Generic;
using System.Web.Http;

namespace Galleria.Profiles.Api.Service
{
    /// <summary>
    /// A class that provides the API for managing User Profiles.
    /// </summary>
    public sealed class UserProfileController : ApiController
    {
        private readonly IUserProfileRepository _userProfileRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileController"/> class.
        /// </summary>
        public UserProfileController()
        {
            // TODO: Move this to be injected.
            var connection = ConnectionFactory.CreateConnection();
            _userProfileRepository = new UserProfileRepository(connection);
        }

        /// <summary>
        /// Gets all user profiles.
        /// </summary>
        /// <returns>A collection of user profiles.</returns>
        [HttpGet]
        [Route("api/users")]
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            return _userProfileRepository.GetUserProfiles();
        }
    }
}