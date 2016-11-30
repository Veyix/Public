using Galleria.Api.Contract;
using System.Collections.Generic;

namespace Galleria.Api.Service
{
    /// <summary>
    /// Provides a contract for a repository that manages instances of the <see cref="UserProfile"/> class.
    /// </summary>
    public interface IUserProfileRepository
    {
        /// <summary>
        /// Gets a collection of users.
        /// </summary>
        /// <returns>A collection of users.</returns>
        IEnumerable<UserProfile> GetUsers();

        /// <summary>
        /// Gets a collection of users for the specified company.
        /// </summary>
        /// <param name="companyId">The Id of the company for which to get the users.</param>
        /// <returns>A collection of users.</returns>
        IEnumerable<UserProfile> GetUsers(int companyId);

        /// <summary>
        /// Gets a user based on a given Id.
        /// </summary>
        /// <param name="userId">The Id of the user to get.</param>
        /// <returns>The requested user, if found; otherwise null.</returns>
        UserProfile GetUser(int userId);

        /// <summary>
        /// Adds the given user profile to the system.
        /// </summary>
        /// <param name="profile">The profile to be added.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is null.</exception>
        void AddUser(UserProfile profile);

        /// <summary>
        /// Updates the given user profile within the system.
        /// </summary>
        /// <param name="profile">The profile to be updated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is null.</exception>
        void UpdateUser(UserProfile profile);

        /// <summary>
        /// Deletes the given user profile from the system.
        /// </summary>
        /// <param name="profile">The profile to be deleted.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is null.</exception>
        void DeleteUser(UserProfile profile);
    }
}