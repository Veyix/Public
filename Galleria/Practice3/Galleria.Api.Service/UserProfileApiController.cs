using Galleria.Api.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Galleria.Api.Service
{
    public sealed class UserProfileApiController : ApiController, IUserProfileApi
    {
        [HttpPost]
        [Route("api/users")]
        [Authorize(Roles = SecurityRoles.Administrator)]
        public void CreateUser(UserProfile profile)
        {
            using (var context = new DatabaseContext())
            {
                context.Entry(profile).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
        }

        [HttpDelete]
        [Route("api/users/{userId:int}")]
        [Authorize(Roles = SecurityRoles.Administrator)]
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

        [HttpGet]
        [Route("api/users/{userId:int}")]
        [AuthorizeRoles(SecurityRoles.BasicUser, SecurityRoles.Administrator)]
        public UserProfile GetUser(int userId)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<UserProfile>()
                    .SingleOrDefault(x => x.Id == userId);
            }
        }

        [HttpGet]
        [Route("api/users")]
        [AuthorizeRoles(SecurityRoles.BasicUser, SecurityRoles.Administrator)]
        public IEnumerable<UserProfile> GetUsers()
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<UserProfile>()
                    .ToArray();
            }
        }

        [HttpGet]
        [Route("api/company/{companyId:int}/users")]
        [AuthorizeRoles(SecurityRoles.BasicUser, SecurityRoles.Administrator)]
        public IEnumerable<UserProfile> GetUsers(int companyId)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<UserProfile>()
                    .Where(x => x.CompanyId == companyId)
                    .ToArray();
            }
        }

        [HttpPut]
        [Route("api/users")]
        [AuthorizeRoles(SecurityRoles.BasicUser, SecurityRoles.Administrator)]
        public void UpdateUser(UserProfile profile)
        {
            using (var context = new DatabaseContext())
            {
                context.Entry(profile).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}