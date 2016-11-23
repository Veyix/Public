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
        private readonly IEntityStore _entityStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileRepository"/> class.
        /// </summary>
        /// <param name="queryProvider">The instance that creates entity queries.</param>
        /// <param name="entityStore">A store of entities.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the given parameters are null.</exception>
        public UserProfileRepository(IQueryProvider queryProvider, IEntityStore entityStore)
        {
            Verify.NotNull(queryProvider, nameof(queryProvider));
            Verify.NotNull(entityStore, nameof(entityStore));

            _queryProvider = queryProvider;
            _entityStore = entityStore;
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

        public void AddUser(UserProfile profile)
        {
            Verify.NotNull(profile, nameof(profile));

            _entityStore.AddEntity(profile);
            _entityStore.Save();
        }
    }
}