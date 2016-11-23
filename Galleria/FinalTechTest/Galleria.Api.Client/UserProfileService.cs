using Galleria.Api.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Galleria.Api.Client
{
    /// <summary>
    /// A class that wraps the calls to the user profile API.
    /// </summary>
    public sealed class UserProfileService : IDisposable
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileService"/> class.
        /// </summary>
        /// <param name="serviceAddress">The address where the service is hosted.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="serviceAddress"/> is null or empty.</exception>
        public UserProfileService(string serviceAddress)
        {
            Verify.NotNullOrEmpty(serviceAddress, nameof(serviceAddress));

            _client = new HttpClient();
            _client.BaseAddress = new Uri(serviceAddress);
        }

        /// <summary>
        /// Logs into the hosted API using the given credentials.
        /// </summary>
        /// <param name="username">The username to use.</param>
        /// <param name="password">The password to use.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> or
        /// <paramref name="password"/> is null or empty.</exception>
        public void Login(string username, string password)
        {
            Verify.NotNullOrEmpty(username, nameof(username));
            Verify.NotNullOrEmpty(password, nameof(password));

            // TODO
        }

        /// <summary>
        /// Gets all users from the API.
        /// </summary>
        /// <returns>A collection of user profiles.</returns>
        public IEnumerable<UserProfile> GetUsers()
        {
            var task = _client.GetAsync("api/users");
            return GetResult<IEnumerable<UserProfile>>(task);
        }

        /// <summary>
        /// Gets all users for the specified company.
        /// </summary>
        /// <param name="companyId">The Id of the company for which to get the users.</param>
        /// <returns>A collection of user profiles.</returns>
        public IEnumerable<UserProfile> GetUsers(int companyId)
        {
            var task = _client.GetAsync($"api/company/{companyId}/users");
            return GetResult<IEnumerable<UserProfile>>(task);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        private static TResult GetResult<TResult>(Task<HttpResponseMessage> task)
        {
            task.Wait();

            if (!task.Result.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Operation Failed: " + task.Result.ReasonPhrase);
            }

            var resultTask = task.Result.Content.ReadAsStringAsync();
            resultTask.Wait();

            if (String.IsNullOrWhiteSpace(resultTask.Result))
            {
                throw new InvalidOperationException($"No result received. Status Code {task.Result.StatusCode}");
            }

            // Read the result as a JSON object
            return JsonConvert.DeserializeObject<TResult>(resultTask.Result);
        }
    }
}