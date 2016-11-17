using Galleria.Api.Contract;
using System;
using System.Collections.Generic;

namespace Galleria.Api.Server
{
    public sealed class UserProfileRepository
    {
        public IEnumerable<UserProfile> GetUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserProfile> GetUsers(int companyId)
        {
            throw new NotImplementedException();
        }

        public UserProfile GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public void SaveUser(UserProfile profile)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}