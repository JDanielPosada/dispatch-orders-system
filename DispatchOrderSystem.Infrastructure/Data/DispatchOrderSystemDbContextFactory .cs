using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DispatchOrderSystem.Infrastructure.Data
{
    public class DispatchOrderSystemDbContextFactory: IDesignTimeDbContextFactory<DispatchOrderSystemDbContext>
    {
        public DispatchOrderSystemDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DispatchOrderSystemDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new DispatchOrderSystemDbContext(optionsBuilder.Options);
        }
    }
}
