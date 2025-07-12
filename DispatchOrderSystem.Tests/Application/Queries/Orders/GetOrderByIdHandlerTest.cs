using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Queries.Orders;
using DispatchOrderSystem.Application.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Queries.Orders
{
    public class GetOrderByIdHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsOrderDetail_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var expectedOrder = new OrderDetailDto
            {
                OrderId = orderId,
                ClientName = "Cliente Test",
                ProductName = "Producto Test",
                DistanceKm = 100,
                Cost = 20000,
                CreatedAt = DateTime.UtcNow
            };

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(s => s.GetOrderDetailByIdAsync(orderId))
                .ReturnsAsync(expectedOrder);

            var handler = new GetOrderByIdHandler(mockOrderService.Object);
            var request = new GetOrderByIdQueryRequest(orderId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderId);
            Assert.Equal("Cliente Test", result.ClientName);
            Assert.Equal("Producto Test", result.ProductName);
            mockOrderService.Verify(s => s.GetOrderDetailByIdAsync(orderId), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsNull_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(s => s.GetOrderDetailByIdAsync(orderId))
                .ReturnsAsync((OrderDetailDto?)null);

            var handler = new GetOrderByIdHandler(mockOrderService.Object);
            var request = new GetOrderByIdQueryRequest(orderId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Null(result);
            mockOrderService.Verify(s => s.GetOrderDetailByIdAsync(orderId), Times.Once);
        }
    }
}
