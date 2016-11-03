using System.Collections.Generic;
using System.Web.Http;

namespace Galleria.Api
{
    public class UsersController : ApiController
    {
        private readonly UserRepository _userRepository = new UserRepository();

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        [HttpGet]
        public User GetUser(int id)
        {
            return _userRepository.GetUserById(id);
        }
    }
}