namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that represents a user with security access.
    /// </summary>
    public sealed class SecurityUser
    {
        /// <summary>
        /// Gets or sets the Id of the security user record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the security role the user holds.
        /// </summary>
        public string Role { get; set; }
    }
}