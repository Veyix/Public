using Galleria.Api.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Galleria.Api.Service
{
    public sealed class UserProfileRepository
    {
        private static List<UserProfile> Profiles =
            new List<UserProfile>()
            {
                new UserProfile()
                {
                    UserId = 1,
                    CompanyId = 1,
                    Title = "Mr",
                    Forename = "Samuel",
                    Surname = "Slade",
                    DateOfBirth = new DateTime(1988, 9, 13)
                }
            };

        public IEnumerable<UserProfile> GetUsers()
        {
            return Profiles.ToArray();
        }

        public IEnumerable<UserProfile> GetUsers(int companyId)
        {
            return Profiles.Where(x => x.CompanyId == companyId).ToArray();
        }

        public UserProfile GetUser(int userId)
        {
            return Profiles.SingleOrDefault(x => x.UserId == userId);
        }

        public void CreateUser(UserProfile profile)
        {
            profile.UserId = Profiles.Max(x => x.UserId) + 1;
            Profiles.Add(profile);
        }

        public void UpdateUser(UserProfile profile)
        {
            int index = Profiles.IndexOf(profile);
            if (index >= 0)
            {
                Profiles[index] = profile;
            }
        }

        public void DeleteUser(int userId)
        {
            var user = GetUser(userId);
            if (user != null)
            {
                Profiles.Remove(user);
            }
        }
    }
}