using Galleria.Profiles.ObjectModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Galleria.Profiles.Api.Client
{
    /// <summary>
    /// A class that handles the client interaction with the user profile API.
    /// </summary>
    public sealed class UserProfileService : IUserProfileService
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileService"/> class.
        /// </summary>
        /// <param name="address">The base address at which the API is hosted.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="address"/> is null or empty.</exception>
        public UserProfileService(string address)
        {
            if (String.IsNullOrWhiteSpace(address)) throw new ArgumentException("The service address cannot be empty", nameof(address));

            _client = new HttpClient();
            _client.BaseAddress = new Uri(address);
        }

        /// <summary>
        /// Gets all user profiles.
        /// </summary>
        /// <returns>A collection of user profiles.</returns>
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            var task = _client.GetStringAsync("api/users");
            task.Wait();

            return JsonConvert.DeserializeObject<IEnumerable<UserProfile>>(task.Result);
        }

        /// <summary>
        /// Gets the user profiles for the specified company.
        /// </summary>
        /// <param name="companyId">The Id of the company for which to get the user profiles.</param>
        /// <returns>A collection of user profiles.</returns>
        public IEnumerable<UserProfile> GetUserProfilesByCompanyId(int companyId)
        {
            var task = _client.GetStringAsync($"api/company/{companyId}/users");
            task.Wait();

            return JsonConvert.DeserializeObject<IEnumerable<UserProfile>>(task.Result);
        }

        /// <summary>
        /// Gets the specified user profile.
        /// </summary>
        /// <param name="userId">The Id of the user profile to get.</param>
        /// <returns>The specified user profile, if found; otherwise null..</returns>
        public UserProfile GetUserProfile(int userId)
        {
            var task = _client.GetStringAsync($"api/users/{userId}");
            task.Wait();

            return JsonConvert.DeserializeObject<UserProfile>(task.Result);
        }
    }
}