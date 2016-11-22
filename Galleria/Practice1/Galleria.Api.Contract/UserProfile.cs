using System;

namespace Galleria.Api.Contract
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastChangedDate { get; set; }
    }
}