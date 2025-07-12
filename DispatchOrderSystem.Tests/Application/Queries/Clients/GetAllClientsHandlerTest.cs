using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Queries.Clients;
using DispatchOrderSystem.Application.Services.Interfaces;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Queries.Clients
{
    public class GetAllClientsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsListOfClients()
        {
            // Arrange
            var mockClientService = new Mock<IClientService>();
            var expectedClients = new List<ClientDto>
        {
            new ClientDto { Id = Guid.NewGuid(), Name = "Cliente 1" },
            new ClientDto { Id = Guid.NewGuid(), Name = "Cliente 2" }
        };

            mockClientService
                .Setup(service => service.GetAllClientsAsync())
                .ReturnsAsync(expectedClients);

            var handler = new GetAllClientsQueryHandler(mockClientService.Object);
            var request = new GetAllClientsQueryRequest();

            // Act
            var result = await handler.Handle(request, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedClients.Count, result.Count());
            Assert.Equal(expectedClients.First().Name, result.First().Name);
            mockClientService.Verify(service => service.GetAllClientsAsync(), Times.Once);
        }
    }
}
