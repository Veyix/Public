using Galleria.Api.Contract;
using Newtonsoft.Json;
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

            _client.PostAsync("api/users", content).Wait();
        }

        public void UpdateUser(UserProfile profile)
        {
            Verify.NotNull(profile, nameof(profile));

            var serializedProfile = JsonConvert.SerializeObject(profile);
            var content = new StringContent(serializedProfile);

            _client.PutAsync("api/users", content).Wait();
        }

        public void DeleteUser(int userId)
        {
            _client.DeleteAsync($"api/users/{userId}").Wait();
        }

        private static T GetResult<T>(Task<HttpResponseMessage> task)
        {
            task.Wait();

            var resultTask = task.Result.Content.ReadAsStringAsync();
            resultTask.Wait();

            return JsonConvert.DeserializeObject<T>(resultTask.Result);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}