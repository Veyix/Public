using System;

namespace Galleria.Api.Contract
{
    /// <summary>
    /// A class that represents the profile of a user.
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Gets or sets the Id of the user record.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Id of the company to which the user belongs.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the user's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string Forename { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the user's date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}