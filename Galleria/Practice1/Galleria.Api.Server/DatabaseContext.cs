using Galleria.Api.Contract;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace Galleria.Api.Server
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(string connectionString)
            : base(connectionString)
        {
        }

        public IQueryable<TEntity> CreateQuery<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        public void Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            Entry(entity).State = EntityState.Added;

            Save();
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;

            Save();
        }

        public void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;

            Save();
        }

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MapUserProfile(modelBuilder.Entity<UserProfile>());
        }

        private static void MapUserProfile(EntityTypeConfiguration<UserProfile> configurator)
        {
            configurator.HasKey(x => x.UserId);

            configurator.Property(x => x.UserId)
                .HasColumnName("Id")
                .IsRequired();

            configurator.Property(x => x.CompanyId)
                .HasColumnName("CompanyId")
                .IsRequired();

            configurator.Property(x => x.Title)
                .HasColumnName("Title")
                .HasMaxLength(50)
                .IsRequired();

            configurator.Property(x => x.Forename)
                .HasColumnName("Forename")
                .HasMaxLength(100)
                .IsRequired();

            configurator.Property(x => x.Surname)
                .HasColumnName("Surname")
                .HasMaxLength(100)
                .IsRequired();

            configurator.Property(x => x.DateOfBirth)
                .HasColumnName("DateOfBirth")
                .IsRequired();

            configurator.Property(x => x.CreatedDate)
                .HasColumnName("CreatedDate")
                .IsRequired();

            configurator.Property(x => x.LastChangedDate)
                .HasColumnName("LastChangedDate")
                .IsRequired();

            configurator.ToTable(nameof(UserProfile));
        }
    }
}