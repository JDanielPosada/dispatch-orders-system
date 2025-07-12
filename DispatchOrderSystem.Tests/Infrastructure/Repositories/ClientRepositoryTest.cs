using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Infrastructure.Data;
using DispatchOrderSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DispatchOrderSystem.Tests.Infrastructure.Repositories
{
    public class ClientRepositoryTests
    {
        private readonly DbContextOptions<DispatchOrderSystemDbContext> _dbContextOptions;

        public ClientRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<DispatchOrderSystemDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        private Client CreateClient(Guid? id = null)
        {
            return new Client
            {
                Id = id ?? Guid.NewGuid(),
                Name = "Cliente de prueba",
            };
        }

        [Fact]
        public async Task AddAsync_Should_Add_Client()
        {
            using var context = new DispatchOrderSystemDbContext(_dbContextOptions);
            var repository = new ClientRepository(context);

            var client = CreateClient();

            await repository.AddAsync(client);

            var result = await context.Clients.FindAsync(client.Id);
            Assert.NotNull(result);
            Assert.Equal(client.Name, result!.Name);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Client_When_Exists()
        {
            var client = CreateClient();

            using (var context = new DispatchOrderSystemDbContext(_dbContextOptions))
            {
                context.Clients.Add(client);
                await context.SaveChangesAsync();
            }

            using (var context = new DispatchOrderSystemDbContext(_dbContextOptions))
            {
                var repository = new ClientRepository(context);
                var result = await repository.GetByIdAsync(client.Id);

                Assert.NotNull(result);
                Assert.Equal(client.Name, result!.Name);
            }
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null_When_Not_Exists()
        {
            using var context = new DispatchOrderSystemDbContext(_dbContextOptions);
            var repository = new ClientRepository(context);

            var result = await repository.GetByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Clients()
        {
            var clients = new List<Client>
            {
                CreateClient(),
                CreateClient(),
                CreateClient()
            };

            using (var context = new DispatchOrderSystemDbContext(_dbContextOptions))
            {
                context.Clients.AddRange(clients);
                await context.SaveChangesAsync();
            }

            using (var context = new DispatchOrderSystemDbContext(_dbContextOptions))
            {
                var repository = new ClientRepository(context);
                var result = await repository.GetAllAsync();

                Assert.Equal(3, result.Count);
            }
        }
    }
}
