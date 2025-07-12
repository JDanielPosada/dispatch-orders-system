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
    public class GetAllOrderQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsListOfOrderDetails()
        {
            // Arrange
            var expectedOrders = new List<OrderDetailDto>
        {
            new OrderDetailDto
            {
                OrderId = Guid.NewGuid(),
                ClientName = "Cliente 1",
                ProductName = "Producto 1",
                DistanceKm = 15.5,
                Cost = 1000
            },
            new OrderDetailDto
            {
                OrderId = Guid.NewGuid(),
                ClientName = "Cliente 2",
                ProductName = "Producto 2",
                DistanceKm = 20,
                Cost = 1500
            }
        };

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(service => service.GetAllOrderDetailsAsync())
                .ReturnsAsync(expectedOrders);

            var handler = new GetAllOrderQueryHandler(mockOrderService.Object);
            var request = new GetAllOrdersQueryRequest();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(expectedOrders, result);
            mockOrderService.Verify(service => service.GetAllOrderDetailsAsync(), Times.Once);
        }
    }
}
