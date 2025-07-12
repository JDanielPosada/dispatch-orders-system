using DispatchOrderSystem.Api.Controllers;
using DispatchOrderSystem.Application.Commands.Seed;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Api.Controllers
{
    public class SeedOrdersControllerTests
    {
        [Fact]
        public async Task Post_ReturnsOkResult_WithSuccessMessage()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(m => m.Send(It.IsAny<SeedOrdersCommandRequest>(), default))
                .ReturnsAsync("Datos sembrados correctamente");

            var controller = new SeedOrdersController(mockMediator.Object);

            // Act
            var actionResult = await controller.Post();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult); // 👈 Aquí definimos okResult correctamente
            var response = okResult.Value;

            // Como es un tipo anónimo, usamos reflexión para acceder a la propiedad
            var messageProperty = response?.GetType().GetProperty("message")?.GetValue(response, null)?.ToString();

            Assert.Equal("Datos sembrados correctamente", messageProperty);
        }
    }
}
