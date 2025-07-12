using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Infrastructure.Data;
using DispatchOrderSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DispatchOrderSystem.Tests.Infrastructure.Repositories
{
    public class ProductRepositoryTests
    {
        private DbContextOptions<DispatchOrderSystemDbContext> _dbContextOptions;

        public ProductRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<DispatchOrderSystemDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Asegura DB limpia por test
                .Options;
        }

        private Product CreateProduct()
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test Product",
                Description = "Description A"
            };
        }

        [Fact]
        public async Task AddAsync_Should_Add_Product()
        {
            using var context = new DispatchOrderSystemDbContext(_dbContextOptions);
            var repository = new ProductRepository(context);

            var product = CreateProduct();

            await repository.AddAsync(product);

            var result = await context.Products.FindAsync(product.Id);
            Assert.NotNull(result);
            Assert.Equal(product.Name, result!.Name);
            Assert.Equal(product.Description, result.Description);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Product()
        {
            using var context = new DispatchOrderSystemDbContext(_dbContextOptions);
            var repository = new ProductRepository(context);

            var product = CreateProduct();
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var result = await repository.GetByIdAsync(product.Id);

            Assert.NotNull(result);
            Assert.Equal(product.Name, result!.Name);
            Assert.Equal(product.Description, result.Description);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Products()
        {
            using var context = new DispatchOrderSystemDbContext(_dbContextOptions);
            var repository = new ProductRepository(context);

            var products = new[]
            {
            CreateProduct(),
            CreateProduct()
        };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();

            var result = await repository.GetAllAsync();

            Assert.Equal(2, result.Count);
        }
    }
}
