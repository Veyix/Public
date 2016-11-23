using Galleria.Api.Contract;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that manages the context of database connections.
    /// </summary>
    internal sealed class DatabaseContext : DbContext, IQueryProvider, IEntityStore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        public DatabaseContext()
            : base(nameOrConnectionString: "Galleria")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MapUserProfile(modelBuilder.Entity<UserProfile>());
        }

        private static void MapUserProfile(EntityTypeConfiguration<UserProfile> configuration)
        {
            configuration.HasKey(x => x.UserId);
            configuration.Property(x => x.UserId).HasColumnName("Id");
            configuration.ToTable("UserProfile");
        }

        IQueryable<TEntity> IQueryProvider.CreateQuery<TEntity>()
        {
            return Set<TEntity>();
        }

        void IEntityStore.AddEntity<TEntity>(TEntity entity)
        {
            Entry(entity).State = EntityState.Added;
        }

        void IEntityStore.Save()
        {
            SaveChanges();
        }
    }
}