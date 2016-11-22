using Galleria.Api.Contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Galleria.Api.Client
{
    public sealed class UserProfileApiClient : IUserProfileApi, IDisposable
    {
        private readonly HttpClient _client;

        public UserProfileApiClient(string address)
        {
            Verify.NotNullOrEmpty(address, nameof(address));

            _client = new HttpClient();
            _client.BaseAddress = new Uri(address);
        }

        public UserProfile GetUser(int userId)
        {
            var task = _client.GetAsync($"api/user/{userId}");
            return GetResult<UserProfile>(task);
        }

        public IEnumerable<UserProfile> GetUsers()
        {
            var task = _client.GetAsync("api/users");
            return GetResult<IEnumerable<UserProfile>>(task);
        }

        public IEnumerable<UserProfile> GetUsers(int companyId)
        {
            var task = _client.GetAsync($"api/company/{companyId}/users");
            return GetResult<IEnumerable<UserProfile>>(task);
        }

        public void CreateUser(UserProfile profile)
        {
            Verify.NotNull(profile, nameof(profile));

            var serializedProfile = JsonConvert.SerializeObject(profile);
            var content = new StringContent(serializedProfile);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var task = _client.PostAsync("api/users", content);
            task.Wait();

            HandleResponse(task.Result);
        }

        public void UpdateUser(UserProfile profile)
        {
            Verify.NotNull(profile, nameof(profile));

            var serializedProfile = JsonConvert.SerializeObject(profile);
            var content = new StringContent(serializedProfile);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var task = _client.PutAsync("api/users", content);
            task.Wait();

            HandleResponse(task.Result);
        }

        public void DeleteUser(int userId)
        {
            var task = _client.DeleteAsync($"api/users/{userId}");
            task.Wait();

            HandleResponse(task.Result);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        private static T GetResult<T>(Task<HttpResponseMessage> task)
        {
            if (!HandleResponse(task.Result))
            {
                return default(T);
            }

            task.Wait();

            var resultTask = task.Result.Content.ReadAsStringAsync();
            resultTask.Wait();

            return JsonConvert.DeserializeObject<T>(resultTask.Result);
        }

        private static bool HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                SignalError($"Error: {response.StatusCode}");

                return false;
            }

            var resultTask = response.Content.ReadAsStringAsync();
            resultTask.Wait();

            try
            {
                var jsonObject = JObject.Parse(resultTask.Result);
                JToken errorDescription;

                if (jsonObject.TryGetValue("error_description", out errorDescription))
                {
                    SignalError(errorDescription.Value<string>());

                    return false;
                }
            }
            catch
            {
                //SignalError($"Unknown response: {resultTask.Result}");
            }

            return true;
        }

        private static void SignalError(string errorDescription)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorDescription);
            Console.ResetColor();
        }
    }
}