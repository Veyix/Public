using Galleria.Api.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that provides access to the API to manage user profiles through a Web API 2 controller.
    /// </summary>
    public sealed class UserProfileApiController : ApiController
    {
        private static readonly IEnumerable<UserProfile> Users = new[]
            {
                new UserProfile()
                {
                    UserId = 1,
                    CompanyId = 1,
                    Title = "Mr",
                    Forename = "Test",
                    Surname = "Tester",
                    DateOfBirth = DateTime.Today.AddYears(-20)
                }
            };

        /// <summary>
        /// Retrieves a collection of all users.
        /// </summary>
        /// <returns>A response containing the users.</returns>
        [HttpGet]
        [Route("api/users")]
        [AuthorizeRoles(SecurityRoles.BasicUser, SecurityRoles.Administrator)]
        public IHttpActionResult GetUsers()
        {
            return Ok(Users);
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
            var users = Users.Where(x => x.CompanyId == companyId);
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
            var user = Users.SingleOrDefault(x => x.UserId == userId);
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

            // TODO: Add the user to the system.

            return Ok();
        }
    }
}