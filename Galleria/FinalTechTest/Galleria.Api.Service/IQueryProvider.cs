using System.Linq;

namespace Galleria.Api.Service
{
    /// <summary>
    /// Provides a contract for a provider of entity queries.
    /// </summary>
    public interface IQueryProvider
    {
        /// <summary>
        /// Creates a query for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity for which to create a query.</typeparam>
        /// <returns>A query for the specified entity type.</returns>
        IQueryable<TEntity> CreateQuery<TEntity>()
            where TEntity : class;
    }
}