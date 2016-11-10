using Galleria.Profiles.ObjectModel;
using System.Collections.Generic;

namespace Galleria.Profiles.Api.Client
{
    /// <summary>
    /// Provides a contract for a client to the user profile API.
    /// </summary>
    public interface IUserProfileService
    {
        /// <summary>
        /// Gets all user profiles.
        /// </summary>
        /// <returns>A collection of user profiles.</returns>
        IEnumerable<UserProfile> GetUserProfiles();

        /// <summary>
        /// Gets the specified user profile.
        /// </summary>
        /// <param name="userId">The Id of the user profile to get.</param>
        /// <returns>The specified user profile, if found; otherwise null..</returns>
        UserProfile GetUserProfile(int userId);

        /// <summary>
        /// Gets the user profiles for the specified company.
        /// </summary>
        /// <param name="companyId">The Id of the company for which to get the user profiles.</param>
        /// <returns>A collection of user profiles.</returns>
        IEnumerable<UserProfile> GetUserProfilesByCompanyId(int companyId);
    }
}