using System.Linq;

namespace Galleria.Api
{
    public class UserRepository
    {
        private static readonly User[] _users =
        {
            new User()
            {
                Id = 101,
                Username = "veyix",
                Forename = "Samuel",
                Surname = "Slade"
            },
            new User()
            {
                Id = 593,
                Username = "greenvalley",
                Forename = "Wyatt",
                Surname = "GreenValley"
            }
        };

        public User[] GetUsers()
        {
            return _users;
        }

        public User GetUserById(int userId)
        {
            return _users.SingleOrDefault(user => user.Id == userId);
        }
    }
}