using DispatchOrderSystem.Domain.Aggregates.Entities;
using Microsoft.EntityFrameworkCore;

namespace DispatchOrderSystem.Infrastructure.Data
{
    public class DispatchOrderSystemDbContext : DbContext
    {
        public DispatchOrderSystemDbContext(DbContextOptions<DispatchOrderSystemDbContext> options)
    :       base(options) { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().OwnsOne(o => o.Origin);
            modelBuilder.Entity<Order>().OwnsOne(o => o.Destination);
        }
    }
}
