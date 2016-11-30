namespace Galleria.Api.Service
{
    /// <summary>
    /// Provides a contract for a repository that manages instances of the <see cref="SecurityUser"/> class.
    /// </summary>
    public interface ISecurityUserRepository
    {
        /// <summary>
        /// Gets the security user with the matching credentials.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The matching security user, if found; otherwise null.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> or
        /// <paramref name="password"/> is null or empty.</exception>
        SecurityUser GetSecurityUser(string username, string password);
    }
}