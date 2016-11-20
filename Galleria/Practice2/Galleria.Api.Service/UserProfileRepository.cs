using Galleria.Api.Contract;
using System.Collections.Generic;
using System.Linq;

namespace Galleria.Api.Service
{
    public sealed class UserProfileRepository
    {
        public IEnumerable<UserProfile> GetUsers()
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<UserProfile>().ToArray();
            }
        }

        public IEnumerable<UserProfile> GetUsers(int companyId)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<UserProfile>()
                    .Where(x => x.CompanyId == companyId)
                    .ToArray();
            }
        }

        public UserProfile GetUser(int userId)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<UserProfile>()
                    .SingleOrDefault(x => x.UserId == userId);
            }
        }

        public void CreateUser(UserProfile profile)
        {
            using (var context = new DatabaseContext())
            {
                context.Entry(profile).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
        }

        public void UpdateUser(UserProfile profile)
        {
            using (var context = new DatabaseContext())
            {
                context.Entry(profile).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            var user = GetUser(userId);
            if (user != null)
            {
                using (var context = new DatabaseContext())
                {
                    context.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }
    }
}