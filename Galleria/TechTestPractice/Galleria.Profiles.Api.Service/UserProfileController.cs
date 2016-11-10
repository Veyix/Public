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

        /// <summary>
        /// Gets the specified user profile.
        /// </summary>
        /// <param name="userId">The Id of the user profile to get.</param>
        /// <returns>The specified user profile, if found; otherwise null..</returns>
        [HttpGet]
        [Route("api/users/{userId:int}")]
        public UserProfile GetUserProfile(int userId)
        {
            return _userProfileRepository.GetUserProfile(userId);
        }

        /// <summary>
        /// Gets the user profiles for the specified company.
        /// </summary>
        /// <param name="companyId">The Id of the company for which to get the user profiles.</param>
        /// <returns>A collection of user profiles.</returns>
        [HttpGet]
        [Route("api/company/{companyId:int}/users")]
        public IEnumerable<UserProfile> GetUserProfilesByCompanyId(int companyId)
        {
            return _userProfileRepository.GetUserProfiles(companyId);
        }
    }
}