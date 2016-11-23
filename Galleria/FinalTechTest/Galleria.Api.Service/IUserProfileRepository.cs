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
    }
}