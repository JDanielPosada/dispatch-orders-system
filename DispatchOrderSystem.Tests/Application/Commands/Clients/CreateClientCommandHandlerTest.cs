using DispatchOrderSystem.Application.Commands.Clients;
using DispatchOrderSystem.Application.Services.Interfaces;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Commands.Clients
{
    public class CreateClientCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsNewClientId()
        {
            // Arrange
            var mockClientService = new Mock<IClientService>();
            var expectedClientId = Guid.NewGuid();
            var clientName = "Cliente de prueba";

            mockClientService
                .Setup(service => service.CreateClientAsync(clientName))
                .ReturnsAsync(expectedClientId);

            var handler = new CreateClientCommandHandler(mockClientService.Object);
            var request = new CreateClientCommandRequest(clientName);

            // Act
            var result = await handler.Handle(request, default);

            // Assert
            Assert.Equal(expectedClientId, result);
            mockClientService.Verify(service => service.CreateClientAsync(clientName), Times.Once);
        }
    }
}
