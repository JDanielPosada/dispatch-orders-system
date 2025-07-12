using DispatchOrderSystem.Application.Services;
using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Domain.Interfaces;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsMappedProducts()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Producto A", Description = "Desc A" },
            new Product { Id = Guid.NewGuid(), Name = "Producto B", Description = "Desc B" }
        };

            _productRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name == "Producto A" && p.Description == "Desc A");
            Assert.Contains(result, p => p.Name == "Producto B" && p.Description == "Desc B");
        }

        [Fact]
        public async Task ExistsAsync_WhenProductExists_ReturnsTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Producto X", Description = "Desc X" };

            _productRepositoryMock
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(product);

            // Act
            var exists = await _productService.ExistsAsync(productId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistsAsync_WhenProductDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _productRepositoryMock
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // Act
            var exists = await _productService.ExistsAsync(productId);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task CreateProductAsync_SavesProductAndReturnsId()
        {
            // Arrange
            var name = "Nuevo Producto";
            var description = "Descripción del producto";
            Product? savedProduct = null;

            _productRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                .Callback<Product>(p => savedProduct = p)
                .Returns(Task.CompletedTask);

            // Act
            var resultId = await _productService.CreateProductAsync(name, description);

            // Assert
            Assert.NotEqual(Guid.Empty, resultId);
            Assert.NotNull(savedProduct);
            Assert.Equal(name, savedProduct!.Name);
            Assert.Equal(description, savedProduct.Description);
        }
    }
}
