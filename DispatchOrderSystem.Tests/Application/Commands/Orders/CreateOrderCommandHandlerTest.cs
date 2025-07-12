using DispatchOrderSystem.Application.Commands.Orders;
using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Commands.Orders
{
    public class CreateOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsOrderResponse()
        {
            // Arrange
            var mockOrderService = new Mock<IOrderService>();
            var createOrderRequest = new CreateOrderRequest
            {
                ClientId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 3,
                OriginLatitude = 2.44,
                OriginLongitude = -76.61,
                DestinationLatitude = 3.45,
                DestinationLongitude = -75.89
            };

            var expectedResponse = new OrderResponse
            {
                OrderId = Guid.NewGuid(),
                ClientName = "Cliente Test",
                ProductName = "Producto Test",
                DistanceKm = 120.5,
                Cost = 35000
            };

            mockOrderService
                .Setup(service => service.CreateOrderAsync(createOrderRequest))
                .ReturnsAsync(expectedResponse);

            var handler = new CreateOrderCommandHandler(mockOrderService.Object);
            var command = new CreateOrderCommandRequest(createOrderRequest);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.OrderId, result.OrderId);
            Assert.Equal(expectedResponse.ClientName, result.ClientName);
            Assert.Equal(expectedResponse.ProductName, result.ProductName);
            Assert.Equal(expectedResponse.DistanceKm, result.DistanceKm);
            Assert.Equal(expectedResponse.Cost, result.Cost);

            mockOrderService.Verify(service => service.CreateOrderAsync(createOrderRequest), Times.Once);
        }
    }
}
