using Galleria.Api.Contract;
using System.Data.Entity;

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

            var builder = modelBuilder.Entity<UserProfile>();
            builder.HasKey(x => x.Id);
            builder.ToTable("UserProfile");
        }
    }
}