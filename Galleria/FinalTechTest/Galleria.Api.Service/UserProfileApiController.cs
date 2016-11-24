using Galleria.Api.Contract;
using System.Web.Http;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that provides access to the API to manage user profiles through a Web API 2 controller.
    /// </summary>
    public sealed class UserProfileApiController : ApiController
    {
        private readonly IUserProfileRepository _userProfileRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileApiController"/> class.
        /// </summary>
        /// <param name="userProfileRepository">A repository that manages instances of the <see cref="UserProfile"/> class.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the given parameters are null.</exception>
        public UserProfileApiController(IUserProfileRepository userProfileRepository)
        {
            Verify.NotNull(userProfileRepository, nameof(userProfileRepository));

            _userProfileRepository = userProfileRepository;
        }

        /// <summary>
        /// Retrieves a collection of all users.
        /// </summary>
        /// <returns>A response containing the users.</returns>
        [HttpGet]
        [Route("api/users")]
        [AuthorizeRoles(SecurityRoles.BasicUser, SecurityRoles.Administrator)]
        public IHttpActionResult GetUsers()
        {
            var users = _userProfileRepository.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a collection of users for the specified company.
        /// </summary>
        /// <param name="companyId">The Id of the company for which to get the users.</param>
        /// <returns>A response containing the users.</returns>
        [HttpGet]
        [Route("api/company/{companyId:int}/users")]
        [AuthorizeRoles(SecurityRoles.BasicUser, SecurityRoles.Administrator)]
        public IHttpActionResult GetUsers(int companyId)
        {
            var users = _userProfileRepository.GetUsers(companyId);
            return Ok(users);
        }

        /// <summary>
        /// Retrieves the specified user profile.
        /// </summary>
        /// <param name="userId">The Id of the user to retrieve.</param>
        /// <returns>A response containing the user if found.</returns>
        [HttpGet]
        [Route("api/users/{userId:int}")]
        [AuthorizeRoles(SecurityRoles.BasicUser, SecurityRoles.Administrator)]
        public IHttpActionResult GetUser(int userId)
        {
            var user = _userProfileRepository.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Creates a new user record with the given information.
        /// </summary>
        /// <param name="profile">The information from which to create the record.</param>
        /// <returns>A response detailing whether the operation completed successfully or not.</returns>
        [HttpPost]
        [Route("api/users")]
        [Authorize(Roles = SecurityRoles.Administrator)]
        public IHttpActionResult CreateUser(UserProfile profile)
        {
            if (profile == null)
            {
                return BadRequest("No user profile was given");
            }

            if (profile.UserId != 0)
            {
                return BadRequest("POST cannot be used to modify an existing user. Use PUT instead.");
            }

            _userProfileRepository.AddUser(profile);

            return Ok();
        }

        /// <summary>
        /// Updates the matching user.
        /// </summary>
        /// <param name="profile">The new values for the user.</param>
        /// <returns>A response describing the result of the action.</returns>
        [HttpPut]
        [Route("api/users")]
        [Authorize(Roles = SecurityRoles.Administrator)]
        public IHttpActionResult UpdateUser(UserProfile profile)
        {
            if (profile == null)
            {
                return BadRequest("No user profile was given");
            }

            if (profile.UserId == 0)
            {
                return BadRequest("PUT cannot be used to create a new user. Use POST instead.");
            }

            _userProfileRepository.UpdateUser(profile);

            return Ok();
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="userId">The Id of the user to delete.</param>
        /// <returns>A response describing the result of the action.</returns>
        [HttpDelete]
        [Route("api/users/{userId:int}")]
        [Authorize(Roles = SecurityRoles.Administrator)]
        public IHttpActionResult DeleteUser(int userId)
        {
            // Get hold of the current user to delete it
            var user = _userProfileRepository.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Delete the user from the system
            _userProfileRepository.DeleteUser(user);

            return Ok();
        }
    }
}