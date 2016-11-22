using Galleria.Api.Contract;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Galleria.Api.Service
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("Galleria")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MapUserProfile(modelBuilder.Entity<UserProfile>());
            MapSecurityUser(modelBuilder.Entity<SecurityUser>());
        }

        private static void MapUserProfile(EntityTypeConfiguration<UserProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("UserProfile");
        }

        private static void MapSecurityUser(EntityTypeConfiguration<SecurityUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("SecurityUser");
        }
    }
}