using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Domain.Aggregates.ValueObjects;
using DispatchOrderSystem.Infrastructure.Data;
using DispatchOrderSystem.Infrastructure.Repositories;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DispatchOrderSystem.Tests.Infrastructure.Repositories
{
    public class OrderRepositoryTests
    {
        private DbContextOptions<DispatchOrderSystemDbContext> _dbContextOptions;

        public OrderRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<DispatchOrderSystemDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        private Order CreateOrder(Guid clientId, Guid productId)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                ClientId = clientId,
                ProductId = productId,
                Quantity = 3,
                CreatedAt = DateTime.UtcNow,
                Origin = new Coordinates(1, 1),
                Destination = new Coordinates(2, 2),
                DistanceKm = 10,
                Cost = 100
            };
        }

        [Fact]
        public async Task AddAsync_Should_Add_Order()
        {
            using var context = new DispatchOrderSystemDbContext(_dbContextOptions);
            var repository = new OrderRepository(context);

            var client = new Client { Id = Guid.NewGuid(), Name = "Client A" };
            var product = new Product { Id = Guid.NewGuid(), Name = "Product A", Description = "Description A" };

            context.Clients.Add(client);
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var order = CreateOrder(client.Id, product.Id);

            await repository.AddAsync(order);

            var savedOrder = await context.Orders.FindAsync(order.Id);
            Assert.NotNull(savedOrder);
            Assert.Equal(order.Quantity, savedOrder.Quantity);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Order_With_Client_And_Product()
        {
            using var context = new DispatchOrderSystemDbContext(_dbContextOptions);
            var repository = new OrderRepository(context);

            var client = new Client { Id = Guid.NewGuid(), Name = "Client A" };
            var product = new Product { Id = Guid.NewGuid(), Name = "Product A", Description = "Description A" };
            var order = CreateOrder(client.Id, product.Id);

            context.Clients.Add(client);
            context.Products.Add(product);
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var result = await repository.GetByIdAsync(order.Id);

            Assert.NotNull(result);
            Assert.Equal(order.Id, result!.Id);
            Assert.NotNull(result.Client);
            Assert.NotNull(result.Product);
        }

        [Fact]
        public async Task GetByClientIdAsync_Should_Return_Orders()
        {
            using var context = new DispatchOrderSystemDbContext(_dbContextOptions);
            var repository = new OrderRepository(context);

            var clientId = Guid.NewGuid();
            var product = new Product { Id = Guid.NewGuid(), Name = "Product A", Description = "Description A" };

            var orders = new[]
            {
            CreateOrder(clientId, product.Id),
            CreateOrder(clientId, product.Id)
        };

            context.Clients.Add(new Client { Id = clientId, Name = "Client A" });
            context.Products.Add(product);
            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();

            var result = await repository.GetByClientIdAsync(clientId);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Order()
        {
            using var context = new DispatchOrderSystemDbContext(_dbContextOptions);
            var repository = new OrderRepository(context);

            var client = new Client { Id = Guid.NewGuid(), Name = "Client A" };
            var product = new Product { Id = Guid.NewGuid(), Name = "Product A", Description = "Description A" };
            var order = CreateOrder(client.Id, product.Id);

            context.Clients.Add(client);
            context.Products.Add(product);
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            await repository.DeleteAsync(order.Id);

            var result = await context.Orders.FindAsync(order.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Orders()
        {
            using var context = new DispatchOrderSystemDbContext(_dbContextOptions);
            var repository = new OrderRepository(context);

            var client = new Client { Id = Guid.NewGuid(), Name = "Client A" };
            var product = new Product { Id = Guid.NewGuid(), Name = "Product A", Description = "Description A" };

            var orders = new[]
            {
            CreateOrder(client.Id, product.Id),
            CreateOrder(client.Id, product.Id)
        };

            context.Clients.Add(client);
            context.Products.Add(product);
            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();

            var result = await repository.GetAllAsync();

            Assert.Equal(2, result.Count);
        }
    }
}
