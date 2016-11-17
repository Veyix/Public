using Galleria.Api.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Galleria.Api.Server
{
    public sealed class UserProfileRepository
    {
        private readonly DatabaseContext _context;

        public UserProfileRepository(DatabaseContext context)
        {
            Verify.NotNull(context, nameof(context));

            _context = context;
        }

        public IEnumerable<UserProfile> GetUsers()
        {
            return _context.CreateQuery<UserProfile>()
                .OrderBy(x => x.UserId)
                .ToArray();
        }

        public IEnumerable<UserProfile> GetUsers(int companyId)
        {
            return _context.CreateQuery<UserProfile>()
                .Where(x => x.CompanyId == companyId)
                .OrderBy(x => x.UserId)
                .ToArray();
        }

        public UserProfile GetUser(int userId)
        {
            return _context.CreateQuery<UserProfile>()
                .SingleOrDefault(x => x.UserId == userId);
        }

        public void SaveUser(UserProfile profile)
        {
            // Update the last changed date
            profile.LastChangedDate = DateTime.Now;

            if (profile.UserId == 0)
            {
                // Add the profile as a new record
                profile.CreatedDate = DateTime.Now;
                _context.Add(profile);
            }
            else
            {
                // Update the existing record with the profile
                _context.Update(profile);
            }
        }

        public void DeleteUser(int userId)
        {
            var user = GetUser(userId);
            if (user != null)
            {
                _context.Delete(user);
            }
        }
    }
}