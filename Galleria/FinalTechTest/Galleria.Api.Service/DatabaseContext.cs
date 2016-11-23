using Galleria.Api.Contract;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace Galleria.Api.Service
{
    /// <summary>
    /// A class that manages the context of database connections.
    /// </summary>
    internal sealed class DatabaseContext : DbContext, IQueryProvider
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

        public IQueryable<TEntity> CreateQuery<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }
    }
}