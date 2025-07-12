using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Queries.Products;
using DispatchOrderSystem.Application.Services.Interfaces;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Queries.Products
{
    public class GetAllProductsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsListOfProducts()
        {
            // Arrange
            var expectedProducts = new List<ProductDto>
        {
            new ProductDto { Id = Guid.NewGuid(), Name = "Producto A", Description = "Desc A" },
            new ProductDto { Id = Guid.NewGuid(), Name = "Producto B", Description = "Desc B" }
        };

            var mockProductService = new Mock<IProductService>();
            mockProductService
                .Setup(s => s.GetAllProductsAsync())
                .ReturnsAsync(expectedProducts);

            var handler = new GetAllProductsQueryHandler(mockProductService.Object);
            var request = new GetAllProductsQueryRequest();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(expectedProducts, result);
            mockProductService.Verify(s => s.GetAllProductsAsync(), Times.Once);
        }
    }
}
