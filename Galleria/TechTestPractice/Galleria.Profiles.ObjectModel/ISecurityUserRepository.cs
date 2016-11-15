namespace Galleria.Profiles.ObjectModel
{
    /// <summary>
    /// Provides a contract for a repository that manages instances of the <see cref="SecurityUser"/> class.
    /// </summary>
    public interface ISecurityUserRepository
    {
        /// <summary>
        /// Gets the security user record with the given username.
        /// </summary>
        /// <param name="username">The username by which to find the security user.</param>
        /// <returns>The specified security user, if found; otherwise null.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> is null or empty.</exception>
        SecurityUser GetByUsername(string username);
    }
}