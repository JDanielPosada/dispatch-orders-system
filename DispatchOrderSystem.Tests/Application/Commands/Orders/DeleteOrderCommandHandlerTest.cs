using DispatchOrderSystem.Application.Commands.Orders;
using DispatchOrderSystem.Application.Services.Interfaces;
using MediatR;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Commands.Orders
{
    public class DeleteOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_CallsDeleteOrderAndReturnsUnit()
        {
            // Arrange
            var mockOrderService = new Mock<IOrderService>();
            var orderId = Guid.NewGuid();

            mockOrderService
                .Setup(service => service.DeleteOrderAsync(orderId))
                .Returns(Task.CompletedTask);

            var handler = new DeleteOrderCommandHandler(mockOrderService.Object);
            var command = new DeleteOrderCommandRequest(orderId);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.Equal(Unit.Value, result);
            mockOrderService.Verify(service => service.DeleteOrderAsync(orderId), Times.Once);
        }
    }
}
