using Galleria.Api.Contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Galleria.Api.Client
{
    public sealed class UserProfileApiClient : IUserProfileApi, IDisposable
    {
        private readonly HttpClient _client;

        public UserProfileApiClient(string serviceAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(serviceAddress);
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

        public UserProfile GetUser(int userId)
        {
            var task = _client.GetAsync($"api/users/{userId}");
            return GetResult<UserProfile>(task);
        }

        public void CreateUser(UserProfile profile)
        {
            string data = JsonConvert.SerializeObject(profile);
            var content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var task = _client.PostAsync("api/users", content);
            HandleResponse(task);
        }

        public void UpdateUser(UserProfile profile)
        {
            string data = JsonConvert.SerializeObject(profile);
            var content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var task = _client.PutAsync("api/users", content);
            HandleResponse(task);
        }

        public void DeleteUser(int userId)
        {
            var task = _client.DeleteAsync($"api/users/{userId}");
            HandleResponse(task);
        }

        private static T GetResult<T>(Task<HttpResponseMessage> task)
        {
            task.Wait();

            var resultTask = task.Result.Content.ReadAsStringAsync();
            resultTask.Wait();

            return JsonConvert.DeserializeObject<T>(resultTask.Result);
        }

        private static void HandleResponse(Task<HttpResponseMessage> task)
        {
            task.Wait();

            var resultTask = task.Result.Content.ReadAsStringAsync();
            resultTask.Wait();

            var result = JObject.Parse(resultTask.Result);

            JToken errorToken;
            if (result.TryGetValue("error_description", out errorToken)
                || result.TryGetValue("error", out errorToken))
            {
                throw new InvalidOperationException(errorToken.Value<string>());
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}