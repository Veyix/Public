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

        public void Login(string username, string password)
        {
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "password"));
            values.Add(new KeyValuePair<string, string>("username", username));
            values.Add(new KeyValuePair<string, string>("password", password));

            var content = new FormUrlEncodedContent(values);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var task = _client.PostAsync("api/login", content);
            HandleResponse(task);
        }

        public void CreateUser(UserProfile profile)
        {
            string data = JsonConvert.SerializeObject(profile);
            var content = new StringContent(data);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var task = _client.PostAsync("api/users", content);
            HandleResponse(task);
        }

        public void DeleteUser(int userId)
        {
            var task = _client.DeleteAsync($"api/users/{userId}");
            HandleResponse(task);
        }

        public UserProfile GetUser(int userId)
        {
            var task = _client.GetAsync($"api/users/{userId}");
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

        public void UpdateUser(UserProfile profile)
        {
            string data = JsonConvert.SerializeObject(profile);
            var content = new StringContent(data);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var task = _client.PutAsync("api/users", content);
            HandleResponse(task);
        }

        private T GetResult<T>(Task<HttpResponseMessage> task)
        {
            task.Wait();

            var resultTask = task.Result.Content.ReadAsStringAsync();
            resultTask.Wait();

            return JsonConvert.DeserializeObject<T>(resultTask.Result);
        }

        private void HandleResponse(Task<HttpResponseMessage> task)
        {
            task.Wait();

            if (!task.Result.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Operation Failed: {task.Result.ReasonPhrase}");
            }

            var resultTask = task.Result.Content.ReadAsStringAsync();
            resultTask.Wait();

            if (String.IsNullOrWhiteSpace(resultTask.Result))
            {
                return;
            }

            var result = JObject.Parse(resultTask.Result);
            JToken error;

            if (result.TryGetValue("error_description", out error)
                || result.TryGetValue("error", out error))
            {
                throw new InvalidOperationException(error.Value<string>());
            }

            JToken accessToken;
            if (result.TryGetValue("access_token", out accessToken))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken.Value<string>());
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}