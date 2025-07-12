using DispatchOrderSystem.Application.Commands.Seed;
using DispatchOrderSystem.Application.Services.Interfaces;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Commands.Seed
{
    public class SeedOrdersHandlerTests
    {
        [Fact]
        public async Task Handle_WhenCalled_ReturnsSuccessMessage()
        {
            // Arrange
            var expectedMessage = "Datos sembrados correctamente";
            var mockSeedService = new Mock<ISeedService>();

            mockSeedService
                .Setup(s => s.SeedOrdersAsync())
                .ReturnsAsync(expectedMessage);

            var handler = new SeedOrdersHandler(mockSeedService.Object);
            var request = new SeedOrdersCommandRequest();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(expectedMessage, result);
            mockSeedService.Verify(s => s.SeedOrdersAsync(), Times.Once);
        }
    }
}
