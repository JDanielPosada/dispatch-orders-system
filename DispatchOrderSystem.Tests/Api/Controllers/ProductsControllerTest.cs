using DispatchOrderSystem.Api.Controllers;
using DispatchOrderSystem.Application.Commands.Products;
using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DispatchOrderSystem.Tests.Api.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProductsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithProductId()
        {
            // Arrange
            var request = new CreateProductCommandRequest("Test Product", "Description");
            var expectedId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.Is<CreateProductCommandRequest>(r => r.Name == request.Name), default))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetAll), createdResult.ActionName);

            // Usar reflexión para obtener el id del tipo anónimo
            var value = createdResult.Value!;
            var idProperty = value.GetType().GetProperty("id");
            Assert.NotNull(idProperty);

            var actualId = idProperty!.GetValue(value);
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithProducts()
        {
            // Arrange
            var products = new List<ProductDto>
            {
                new ProductDto { Id = Guid.NewGuid(), Name = "Product A" },
                new ProductDto { Id = Guid.NewGuid(), Name = "Product B" }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllProductsQueryRequest>(), default))
                .ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProducts = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okResult.Value);
            Assert.Equal(2, returnedProducts.Count());
        }
    }
}
