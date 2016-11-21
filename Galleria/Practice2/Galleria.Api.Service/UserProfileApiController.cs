using Galleria.Api.Contract;
using System.Collections.Generic;
using System.Web.Http;

namespace Galleria.Api.Service
{
    public sealed class UserProfileApiController : ApiController, IUserProfileApi
    {
        private readonly UserProfileRepository _userProfileRepository = new UserProfileRepository();

        [HttpGet]
        [Route("api/users")]
        public IEnumerable<UserProfile> GetUsers()
        {
            return _userProfileRepository.GetUsers();
        }

        [HttpGet]
        [Route("api/company/{companyId:int}/users")]
        public IEnumerable<UserProfile> GetUsers(int companyId)
        {
            return _userProfileRepository.GetUsers(companyId);
        }

        [HttpGet]
        [Route("api/users/{userId:int}")]
        public UserProfile GetUser(int userId)
        {
            return _userProfileRepository.GetUser(userId);
        }

        [HttpPost]
        [Route("api/users")]
        public void CreateUser(UserProfile profile)
        {
            _userProfileRepository.CreateUser(profile);
        }

        [HttpPut]
        [Route("api/users")]
        public void UpdateUser(UserProfile profile)
        {
            _userProfileRepository.UpdateUser(profile);
        }

        [HttpDelete]
        [Route("api/users/{userId}")]
        public void DeleteUser(int userId)
        {
            _userProfileRepository.DeleteUser(userId);
        }
    }
}