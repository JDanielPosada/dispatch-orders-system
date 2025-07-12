using DispatchOrderSystem.Api.Controllers;
using DispatchOrderSystem.Application.Commands.Clients;
using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Queries.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Api.Controllers
{
    public class ClientsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ClientsController _controller;

        public ClientsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ClientsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithClientId()
        {
            // Arrange
            var request = new CreateClientCommandRequest("Test Client");
            var expectedId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.Is<CreateClientCommandRequest>(r => r.Name == request.Name), default))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetAll), createdResult.ActionName);

            var value = createdResult.Value!;
            var idProperty = value.GetType().GetProperty("id");
            Assert.NotNull(idProperty);

            var actualId = idProperty!.GetValue(value);
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithClients()
        {
            // Arrange
            var clients = new List<ClientDto>
            {
                new ClientDto { Id = Guid.NewGuid(), Name = "Client A" },
                new ClientDto { Id = Guid.NewGuid(), Name = "Client B" }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllClientsQueryRequest>(), default))
                .ReturnsAsync(clients);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedClients = Assert.IsAssignableFrom<IEnumerable<ClientDto>>(okResult.Value);
            Assert.Equal(2, returnedClients.Count());
        }
    }
}
