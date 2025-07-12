using DispatchOrderSystem.Application.Interfaces;
using DispatchOrderSystem.Application.Services;
using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Domain.Interfaces;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Services
{
    public class SeedServiceTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly SeedService _seedService;

        public SeedServiceTests()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();

            _seedService = new SeedService(
                _orderRepositoryMock.Object,
                _clientRepositoryMock.Object,
                _productRepositoryMock.Object
            );
        }

        [Fact]
        public async Task SeedOrdersAsync_ShouldCreateClientsProductsAndOrders_WhenNoneExist()
        {
            // Arrange
            _clientRepositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Client>());

            _productRepositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Product>());

            var addedClients = new List<Client>();
            _clientRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Client>()))
                .Callback<Client>(c => { c.Id = Guid.NewGuid(); addedClients.Add(c); })
                .Returns(Task.CompletedTask);

            var addedProducts = new List<Product>();
            _productRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>()))
                .Callback<Product>(p => { p.Id = Guid.NewGuid(); addedProducts.Add(p); })
                .Returns(Task.CompletedTask);

            _orderRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _seedService.SeedOrdersAsync();

            // Assert
            _clientRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Client>()), Times.AtLeastOnce());
            _productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.AtLeastOnce());
            _orderRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.AtLeastOnce());
            Assert.Contains("órdenes", result);
        }

        [Fact]
        public async Task SeedOrdersAsync_ShouldSkipAddingClientsAndProducts_IfTheyExist()
        {
            // Arrange
            var existingClients = new List<Client>
            {
                new Client { Id = Guid.NewGuid(), Name = "Existing Client" }
            };

            var existingProducts = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Existing Product", Description = "Desc" }
            };

            _clientRepositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(existingClients);

            _productRepositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(existingProducts);

            _orderRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _seedService.SeedOrdersAsync();

            // Assert
            _clientRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Client>()), Times.Never());
            _productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never());
            _orderRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.AtLeastOnce());
            Assert.Contains("órdenes", result);
        }
    }
}
