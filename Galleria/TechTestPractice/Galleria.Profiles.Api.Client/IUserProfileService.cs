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
        /// Logs into the system with the given credentials.
        /// </summary>
        /// <param name="username">The username to use when logging into the system.</param>
        /// <param name="password">The password to use when logging into the system.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> or
        /// <paramref name="password"/> is null or empty.</exception>
        void Login(string username, string password);

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

        /// <summary>
        /// Creates a new user profile record.
        /// </summary>
        /// <param name="profile">The profile to be created.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is null.</exception>
        void CreateUserProfile(UserProfile profile);

        /// <summary>
        /// Updates the associated record with the given user profile.
        /// </summary>
        /// <param name="profile">The profile to be updated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="profile"/> is new.</exception>
        void UpdateUserProfile(UserProfile profile);

        /// <summary>
        /// Deletes the specified user profile.
        /// </summary>
        /// <param name="userId">The Id of the user profile to be deleted.</param>
        void DeleteUserProfile(int userId);
    }
}