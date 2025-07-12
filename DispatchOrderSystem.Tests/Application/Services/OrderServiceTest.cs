using DispatchOrderSystem.Application.Interfaces;
using DispatchOrderSystem.Application.Services;
using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Domain.Interfaces;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderService = new OrderService(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllOrderDetailsAsync_ReturnsMappedList()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order
                {
                    Id = Guid.NewGuid(),
                    Client = new Client { Name = "Cliente 1" },
                    Product = new Product { Name = "Producto 1" },
                    DistanceKm = 100,
                    Cost = 300,
                    CreatedAt = DateTime.UtcNow
                }
            };

            _orderRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetAllOrderDetailsAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Cliente 1", result[0].ClientName);
            Assert.Equal("Producto 1", result[0].ProductName);
        }

        [Fact]
        public async Task GetOrdersByClientIdAsync_ReturnsMappedOrders()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var orders = new List<Order>
            {
                new Order
                {
                    Id = Guid.NewGuid(),
                    Client = new Client { Name = "Cliente A" },
                    Product = new Product { Name = "Producto A" },
                    DistanceKm = 45,
                    Cost = 100,
                    CreatedAt = DateTime.UtcNow
                }
            };

            _orderRepositoryMock.Setup(r => r.GetByClientIdAsync(clientId)).ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetOrdersByClientIdAsync(clientId);

            // Assert
            Assert.Single(result);
            Assert.Equal("Cliente A", result[0].ClientName);
        }

        [Fact]
        public async Task GetOrderDetailByIdAsync_OrderExists_ReturnsMappedDetail()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                Client = new Client { Name = "Cliente X" },
                Product = new Product { Name = "Producto Y" },
                DistanceKm = 150,
                Cost = 300,
                CreatedAt = DateTime.UtcNow
            };

            _orderRepositoryMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderDetailByIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Cliente X", result!.ClientName);
        }

        [Fact]
        public async Task GetOrderDetailByIdAsync_OrderNotFound_ReturnsNull()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _orderRepositoryMock.Setup(r => r.GetByIdAsync(orderId)).ReturnsAsync((Order?)null);

            // Act
            var result = await _orderService.GetOrderDetailByIdAsync(orderId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteOrderAsync_OrderExists_DeletesOrder()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _orderRepositoryMock.Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(new Order { Id = orderId });

            // Act
            await _orderService.DeleteOrderAsync(orderId);

            // Assert
            _orderRepositoryMock.Verify(r => r.DeleteAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task DeleteOrderAsync_OrderDoesNotExist_ThrowsException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _orderRepositoryMock.Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync((Order?)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _orderService.DeleteOrderAsync(orderId));
        }

        [Fact]
        public async Task GetClientDistanceReportAsync_ReturnsGroupedReport()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Client = new Client { Name = "Cliente A" }, DistanceKm = 30 },
                new Order { Client = new Client { Name = "Cliente A" }, DistanceKm = 120 },
                new Order { Client = new Client { Name = "Cliente A" }, DistanceKm = 300 },
                new Order { Client = new Client { Name = "Cliente A" }, DistanceKm = 800 }
            };

            _orderRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetClientDistanceReportAsync();

            // Assert
            Assert.Single(result);
            var report = result.First();
            Assert.Equal(1, report.Orders_1_50_Km);
            Assert.Equal(1, report.Orders_51_200_Km);
            Assert.Equal(1, report.Orders_201_500_Km);
            Assert.Equal(1, report.Orders_501_1000_Km);
            Assert.Equal(4, report.TotalOrders);
        }
    }
}
