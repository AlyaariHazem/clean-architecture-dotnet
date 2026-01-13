using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastracture.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cars> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cars>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.CarName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CarType).HasMaxLength(50);
                entity.Property(e => e.Manufacturer).HasMaxLength(100);
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.CreatedAt).IsRequired();
            });
        }
    }
}
