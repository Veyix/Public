using Galleria.Api.Contract;
using System.Collections.Generic;
using System.Web.Http;

namespace Galleria.Api.Server
{
    public sealed class UserProfileApiController : ApiController, IUserProfileApi
    {
        private readonly UserProfileRepository _userProfileRepository;

        public UserProfileApiController(UserProfileRepository userProfileRepository)
        {
            Verify.NotNull(userProfileRepository, nameof(userProfileRepository));

            _userProfileRepository = userProfileRepository;
        }

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
        public void CreateUser([FromBody] UserProfile profile)
        {
            _userProfileRepository.SaveUser(profile);
        }

        [HttpPut]
        [Route("api/users")]
        public void UpdateUser([FromBody] UserProfile profile)
        {
            _userProfileRepository.SaveUser(profile);
        }

        [HttpDelete]
        [Route("api/users/{userId:int}")]
        public void DeleteUser(int userId)
        {
            _userProfileRepository.DeleteUser(userId);
        }
    }
}