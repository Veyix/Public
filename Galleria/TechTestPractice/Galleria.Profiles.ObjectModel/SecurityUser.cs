namespace Galleria.Profiles.ObjectModel
{
    /// <summary>
    /// A class that represents a user with access to all or part of the system.
    /// </summary>
    public class SecurityUser
    {
        /// <summary>
        /// Gets or sets the Id of the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the username for the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string Password { get; set; }
    }
}