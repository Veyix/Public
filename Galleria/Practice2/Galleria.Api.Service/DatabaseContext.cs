using Galleria.Api.Contract;
using System.Data.Entity;
using System;
using System.Collections.Generic;

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

            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).HasColumnName("Id");
            builder.Property(x => x.CompanyId).HasColumnName("CompanyId");

            builder.Property(x => x.Title)
                .HasColumnName("Title")
                .HasMaxLength(50);

            builder.Property(x => x.Forename)
                .HasColumnName("Forename")
                .HasMaxLength(100);

            builder.Property(x => x.Surname)
                .HasColumnName("Surname")
                .HasMaxLength(100);

            builder.Property(x => x.DateOfBirth).HasColumnName("DateOfBirth");
            builder.ToTable("UserProfile");
        }

        internal IEnumerable<UserProfile> ToArray()
        {
            throw new NotImplementedException();
        }
    }
}