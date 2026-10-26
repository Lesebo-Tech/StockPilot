
using Microsoft.EntityFrameworkCore;
using StockPilot.Api.Models;

namespace StockPilot.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Store prices with two decimal places.
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // Prevent duplicate product SKUs in SQL Server.
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();
        }
    }
}
