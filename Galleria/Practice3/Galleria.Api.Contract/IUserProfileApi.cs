using System.Collections.Generic;

namespace Galleria.Api.Contract
{
    public interface IUserProfileApi
    {
        IEnumerable<UserProfile> GetUsers();
        IEnumerable<UserProfile> GetUsers(int companyId);
        UserProfile GetUser(int userId);
        void CreateUser(UserProfile profile);
        void UpdateUser(UserProfile profile);
        void DeleteUser(int userId);
    }
}