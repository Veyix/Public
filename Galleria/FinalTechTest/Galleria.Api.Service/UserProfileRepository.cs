using Galleria.Api.Contract;
using System.Collections.Generic;
using System.Linq;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that manages instances of the <see cref="UserProfile"/> class.
    /// </summary>
    internal sealed class UserProfileRepository : IUserProfileRepository
    {
        private readonly IQueryProvider _queryProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileRepository"/> class.
        /// </summary>
        /// <param name="queryProvider">The instance that creates entity queries.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the given parameters are null.</exception>
        public UserProfileRepository(IQueryProvider queryProvider)
        {
            Verify.NotNull(queryProvider, nameof(queryProvider));

            _queryProvider = queryProvider;
        }

        public UserProfile GetUser(int userId)
        {
            return _queryProvider.CreateQuery<UserProfile>()
                .Where(x => x.UserId == userId)
                .SingleOrDefault();
        }

        public IEnumerable<UserProfile> GetUsers()
        {
            return _queryProvider.CreateQuery<UserProfile>()
                .ToArray();
        }

        public IEnumerable<UserProfile> GetUsers(int companyId)
        {
            return _queryProvider.CreateQuery<UserProfile>()
                .Where(x => x.CompanyId == companyId)
                .ToArray();
        }
    }
}