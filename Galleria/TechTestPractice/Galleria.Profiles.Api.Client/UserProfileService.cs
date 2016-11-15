using Galleria.Profiles.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Galleria.Profiles.Api.Client
{
    /// <summary>
    /// A class that handles the client interaction with the user profile API.
    /// </summary>
    public sealed class UserProfileService : IUserProfileService, IDisposable
    {
        private readonly HttpClient _client;
        private string _authenticationScheme;
        private string _authorizationToken;

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
        /// Logs into the system with the given credentials.
        /// </summary>
        /// <param name="username">The username to use when logging into the system.</param>
        /// <param name="password">The password to use when logging into the system.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> or
        /// <paramref name="password"/> is null or empty.</exception>
        public void Login(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username)) throw new ArgumentException("The username cannot be empty", nameof(username));
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentException("The password cannot be empty", nameof(password));

            // Send a form data URL encoded message to the API to gain an authorization token
            var content = new FormUrlEncodedContent(
                new[] {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                }
            );

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var task = _client.PostAsync("api/login", content);
            if (IsFailure(task))
            {
                return;
            }

            var result = HandleResponse(task);
            if (result == null)
            {
                return;
            }

            // Read and store the auth token from the task result
            JToken schemeToken;
            if (result.TryGetValue("token_type", out schemeToken))
            {
                _authenticationScheme = schemeToken.Value<string>();
            }

            JToken authToken;
            if (result.TryGetValue("access_token", out authToken))
            {
                _authorizationToken = authToken.Value<string>();
            }

            if (!String.IsNullOrWhiteSpace(_authenticationScheme) && !String.IsNullOrWhiteSpace(_authorizationToken))
            {
                // Add the auth token to the client's default headers
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(_authenticationScheme, _authorizationToken);
            }
        }

        /// <summary>
        /// Gets all user profiles.
        /// </summary>
        /// <returns>A collection of user profiles.</returns>
        public IEnumerable<UserProfile> GetUserProfiles()
        {
            var task = _client.GetStringAsync("api/users");

            if (IsFailure(task))
            {
                return Enumerable.Empty<UserProfile>();
            }

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

            if (IsFailure(task))
            {
                return Enumerable.Empty<UserProfile>();
            }

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

            if (IsFailure(task))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<UserProfile>(task.Result);
        }

        /// <summary>
        /// Creates a new user profile record.
        /// </summary>
        /// <param name="profile">The profile to be created.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is null.</exception>
        public void CreateUserProfile(UserProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            var data = JsonConvert.SerializeObject(profile);
            var content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var task = _client.PostAsync("api/users", content);

            if (IsFailure(task))
            {
                return;
            }

            HandleResponse(task);
        }

        /// <summary>
        /// Updates the associated record with the given user profile.
        /// </summary>
        /// <param name="profile">The profile to be updated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="profile"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="profile"/> is new.</exception>
        public void UpdateUserProfile(UserProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            var data = JsonConvert.SerializeObject(profile);
            var content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var task = _client.PutAsync("api/users", content);

            if (IsFailure(task))
            {
                return;
            }

            HandleResponse(task);
        }

        /// <summary>
        /// Deletes the specified user profile.
        /// </summary>
        /// <param name="userId">The Id of the user profile to be deleted.</param>
        public void DeleteUserProfile(int userId)
        {
            var task = _client.DeleteAsync($"api/users/{userId}");

            if (IsFailure(task))
            {
                return;
            }

            HandleResponse(task);
        }

        private JObject HandleResponse(Task<HttpResponseMessage> task)
        {
            if (task.IsFaulted)
            {
                string error = task.Exception?.Message;
                if (String.IsNullOrWhiteSpace(error))
                {
                    var responseTask = task.Result.Content.ReadAsStringAsync();
                    responseTask.Wait();

                    error = responseTask.Result;
                }

                throw new InvalidOperationException(error);
            }

            var resultTask = task.Result.Content.ReadAsStringAsync();
            resultTask.Wait();

            Console.WriteLine(resultTask.Result);

            JObject resultObject;
            try
            {
                resultObject = JObject.Parse(resultTask.Result);
            }
            catch
            {
                // Ignore - this isn't a JSON response
                return null;
            }

            JToken errorObject;
            if (resultObject.TryGetValue("error_description", out errorObject))
            {
                throw new InvalidOperationException(errorObject.Value<string>());
            }

            return resultObject;
        }

        private bool IsFailure(Task task)
        {
            try
            {
                task.Wait();
            }
            catch (AggregateException exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                string message = String.Join(Environment.NewLine, exception.InnerExceptions.Select(x => x.Message).ToArray());
                Console.WriteLine(message);

                return true;
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception.Message);

                return true;
            }
            finally
            {
                Console.ResetColor();
            }

            return false;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}