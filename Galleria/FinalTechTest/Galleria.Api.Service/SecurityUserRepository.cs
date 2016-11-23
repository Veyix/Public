using Galleria.Api.Contract;
using System.Linq;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that manages instances of the <see cref="SecurityUser"/> class.
    /// </summary>
    internal sealed class SecurityUserRepository : ISecurityUserRepository
    {
        private readonly IQueryProvider _queryProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityUserRepository"/> class.
        /// </summary>
        /// <param name="queryProvider">A provider of entity queries.</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the given parameters are null.</exception>
        public SecurityUserRepository(IQueryProvider queryProvider)
        {
            Verify.NotNull(queryProvider, nameof(queryProvider));

            _queryProvider = queryProvider;
        }

        SecurityUser ISecurityUserRepository.GetSecurityUser(string username, string password)
        {
            Verify.NotNullOrEmpty(username, nameof(username));
            Verify.NotNullOrEmpty(password, nameof(password));

            return _queryProvider.CreateQuery<SecurityUser>()
                .Where(x => x.Username == username)
                .Where(x => x.Password == password)
                .SingleOrDefault();
        }
    }
}