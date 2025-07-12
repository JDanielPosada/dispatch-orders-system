using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Queries.Orders;
using DispatchOrderSystem.Application.Services.Interfaces;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Queries.Orders
{
    public class GetOrdersByClientIdHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsListOfOrdersForClient()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var expectedOrders = new List<OrderDetailDto>
        {
            new OrderDetailDto
            {
                OrderId = Guid.NewGuid(),
                ClientName = "Cliente A",
                ProductName = "Producto A",
                DistanceKm = 120.5,
                Cost = 25000,
                CreatedAt = DateTime.UtcNow
            },
            new OrderDetailDto
            {
                OrderId = Guid.NewGuid(),
                ClientName = "Cliente A",
                ProductName = "Producto B",
                DistanceKm = 50,
                Cost = 15000,
                CreatedAt = DateTime.UtcNow
            }
        };

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(s => s.GetOrdersByClientIdAsync(clientId))
                .ReturnsAsync(expectedOrders);

            var handler = new GetOrdersByClientIdHandler(mockOrderService.Object);
            var request = new GetOrdersByClientIdQueryRequest(clientId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(expectedOrders.Count, result.Count);
            Assert.Equal("Cliente A", result[0].ClientName);
            Assert.Equal("Producto A", result[0].ProductName);
            Assert.Equal(120.5, result[0].DistanceKm);
            Assert.Equal(25000, result[0].Cost);
            mockOrderService.Verify(s => s.GetOrdersByClientIdAsync(clientId), Times.Once);
        }
    }
}
