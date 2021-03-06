﻿using Galleria.Profiles.ObjectModel;
using Galleria.Support;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Galleria.Profiles.Api.Service
{
    /// <summary>
    /// A class that provides the API for managing User Profiles.
    /// </summary>
    public sealed class UserProfileController : ApiController
    {
        private readonly IUserProfileRepository _userProfileRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileController"/> class.
        /// </summary>
        /// <param name="userProfileRepository">A repository for managing instances of the <see cref="UserProfile"/> class.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="userProfileRepository"/> is null.</exception>
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            Verify.NotNull(userProfileRepository, nameof(userProfileRepository));

            _userProfileRepository = userProfileRepository;
        }

        /// <summary>
        /// Gets all user profiles.
        /// </summary>
        /// <returns>A collection of user profiles.</returns>
        [HttpGet]
        [Route("api/users")]
        [AuthorizeRoles(SecurityRoles.Administrator, SecurityRoles.BasicUser)]
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            return _userProfileRepository.GetUserProfiles();
        }

        /// <summary>
        /// Gets the specified user profile.
        /// </summary>
        /// <param name="userId">The Id of the user profile to get.</param>
        /// <returns>The specified user profile, if found; otherwise null..</returns>
        [HttpGet]
        [Route("api/users/{userId:int}")]
        [AuthorizeRoles(SecurityRoles.Administrator, SecurityRoles.BasicUser)]
        public UserProfile GetUserProfile(int userId)
        {
            return _userProfileRepository.GetUserProfile(userId);
        }

        /// <summary>
        /// Gets the user profiles for the specified company.
        /// </summary>
        /// <param name="companyId">The Id of the company for which to get the user profiles.</param>
        /// <returns>A collection of user profiles.</returns>
        [HttpGet]
        [Route("api/company/{companyId:int}/users")]
        [AuthorizeRoles(SecurityRoles.Administrator, SecurityRoles.BasicUser)]
        public IEnumerable<UserProfile> GetUserProfilesByCompanyId(int companyId)
        {
            return _userProfileRepository.GetUserProfiles(companyId);
        }

        /// <summary>
        /// Creates a new user profile record.
        /// </summary>
        /// <param name="profile">The profile to be created.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is null.</exception>
        [HttpPost]
        [Route("api/users")]
        [Authorize(Roles = SecurityRoles.Administrator)]
        public void CreateUserProfile([FromBody] UserProfile profile)
        {
            Verify.NotNull(profile, nameof(profile));

            _userProfileRepository.SaveUserProfile(profile);
        }

        /// <summary>
        /// Updates the associated record with the given user profile.
        /// </summary>
        /// <param name="profile">The profile to be updated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="profile"/> is new.</exception>
        [HttpPut]
        [Route("api/users")]
        [Authorize(Roles = SecurityRoles.Administrator)]
        public void UpdateUserProfile([FromBody] UserProfile profile)
        {
            Verify.NotNull(profile, nameof(profile));

            if (profile.Id == 0)
            {
                throw new InvalidOperationException("Cannot update a new record");
            }

            _userProfileRepository.SaveUserProfile(profile);
        }

        /// <summary>
        /// Deletes the specified user profile.
        /// </summary>
        /// <param name="userId">The Id of the user profile to be deleted.</param>
        [HttpDelete]
        [Route("api/users/{userId:int}")]
        [Authorize(Roles = SecurityRoles.Administrator)]
        public void DeleteUserProfile(int userId)
        {
            _userProfileRepository.DeleteUserProfile(userId);
        }
    }
}