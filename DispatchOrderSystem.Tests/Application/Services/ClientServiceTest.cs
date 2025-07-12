using DispatchOrderSystem.Application.Services;
using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Domain.Interfaces;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Services
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly ClientService _clientService;

        public ClientServiceTests()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _clientService = new ClientService(_clientRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllClientsAsync_ReturnsMappedClients()
        {
            // Arrange
            var clients = new List<Client>
        {
            new Client { Id = Guid.NewGuid(), Name = "Cliente A" },
            new Client { Id = Guid.NewGuid(), Name = "Cliente B" }
        };

            _clientRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(clients);

            // Act
            var result = await _clientService.GetAllClientsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Name == "Cliente A");
            Assert.Contains(result, c => c.Name == "Cliente B");
        }

        [Fact]
        public async Task ExistsAsync_WhenClientExists_ReturnsTrue()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var client = new Client { Id = clientId, Name = "Cliente Existente" };

            _clientRepositoryMock
                .Setup(repo => repo.GetByIdAsync(clientId))
                .ReturnsAsync(client);

            // Act
            var exists = await _clientService.ExistsAsync(clientId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistsAsync_WhenClientDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var clientId = Guid.NewGuid();

            _clientRepositoryMock
                .Setup(repo => repo.GetByIdAsync(clientId))
                .ReturnsAsync((Client?)null);

            // Act
            var exists = await _clientService.ExistsAsync(clientId);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task CreateClientAsync_SavesClientAndReturnsId()
        {
            // Arrange
            var clientName = "Nuevo Cliente";
            Client? savedClient = null;

            _clientRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Client>()))
                .Callback<Client>(client => savedClient = client)
                .Returns(Task.CompletedTask);

            // Act
            var resultId = await _clientService.CreateClientAsync(clientName);

            // Assert
            Assert.NotEqual(Guid.Empty, resultId);
            Assert.NotNull(savedClient);
            Assert.Equal(clientName, savedClient!.Name);
        }
    }
}
