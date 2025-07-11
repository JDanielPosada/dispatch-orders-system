using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DispatchOrderSystem.Infrastructure.Data;
using DispatchOrderSystem.Application.Interfaces;
using DispatchOrderSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using DispatchOrderSystem.Application.Services;
using DispatchOrderSystem.Domain.Interfaces;


namespace DispatchOrderSystem.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the DbContext with SQL Server
            services.AddDbContext<DispatchOrderSystemDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            // Register application services
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISeedService, SeedService>();
            services.AddScoped<IExcelExportService, ExcelExportService>();

            return services;
        }
    }
}
