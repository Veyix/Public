using System.Collections.Generic;

namespace Galleria.Profiles.ObjectModel
{
    /// <summary>
    /// Provides a contract for a repository that manages instances of the <see cref="UserProfile"/> class.
    /// </summary>
    public interface IUserProfileRepository
    {
        /// <summary>
        /// Gets all of the user profiles.
        /// </summary>
        /// <returns>A read-only collection of user profiles.</returns>
        IReadOnlyCollection<UserProfile> GetUserProfiles();

        /// <summary>
        /// Gets all of the user profiles for the specified company.
        /// </summary>
        /// <param name="companyId">The Id of the company for which to get the user profiles.</param>
        /// <returns>A read-only collection of user profiles.</returns>
        IReadOnlyCollection<UserProfile> GetUserProfiles(int companyId);

        /// <summary>
        /// Gets a specific user profile.
        /// </summary>
        /// <param name="userId">The Id of the user profile to get.</param>
        /// <returns>The specified user profile if found; otherwise null.</returns>
        UserProfile GetUserProfile(int userId);

        /// <summary>
        /// Adds or updates the given user profile record in the database.
        /// </summary>
        /// <param name="profile">The user profile record to be saved.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is null.</exception>
        void SaveUserProfile(UserProfile profile);
    }
}