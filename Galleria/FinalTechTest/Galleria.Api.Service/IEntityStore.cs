namespace Galleria.Api.Service
{
    /// <summary>
    /// Provides a contract for a store of entities.
    /// </summary>
    public interface IEntityStore
    {
        /// <summary>
        /// Adds the given entities to the store.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be added.</typeparam>
        /// <param name="entity">The entity to be added.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="entity"/> is null.</exception>
        void AddEntity<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// Updates the matching record for the given entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to be updated.</typeparam>
        /// <param name="entity">The new values for the entity.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="entity"/> is null.</exception>
        void UpdateEntity<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// Persists all store changes made.
        /// </summary>
        void Save();
    }
}