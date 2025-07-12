using DispatchOrderSystem.Application.Commands.Products;
using DispatchOrderSystem.Application.Services.Interfaces;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Commands.Products
{
    public class CreateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsNewProductId()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var expectedProductId = Guid.NewGuid();
            var name = "Producto Test";
            var description = "Descripción Test";

            mockProductService
                .Setup(service => service.CreateProductAsync(name, description))
                .ReturnsAsync(expectedProductId);

            var handler = new CreateProductCommandHandler(mockProductService.Object);
            var request = new CreateProductCommandRequest(name, description);

            // Act
            var result = await handler.Handle(request, default);

            // Assert
            Assert.Equal(expectedProductId, result);
            mockProductService.Verify(service => service.CreateProductAsync(name, description), Times.Once);
        }
    }
}
