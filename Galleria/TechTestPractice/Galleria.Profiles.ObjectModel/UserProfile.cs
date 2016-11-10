using System;

namespace Galleria.Profiles.ObjectModel
{
    /// <summary>
    /// A class that represents the profile of a user within a company.
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfile"/> class.
        /// </summary>
        public UserProfile()
        {
            CreatedDate = DateTime.Now;
            LastChangedDate = CreatedDate;
        }

        /// <summary>
        /// Gets or sets the Id of the user.
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// Gets or sets the date and time the user profile was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time the user profile was last changed.
        /// </summary>
        public DateTime LastChangedDate { get; set; }
    }
}