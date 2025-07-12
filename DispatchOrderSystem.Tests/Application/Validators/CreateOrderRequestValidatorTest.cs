using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Validators.Orders;
using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Validators
{
    public class CreateOrderRequestValidatorTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly CreateOrderRequestValidator _validator;

        public CreateOrderRequestValidatorTests()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();

            _validator = new CreateOrderRequestValidator(
                _clientRepositoryMock.Object,
                _productRepositoryMock.Object);
        }

        private CreateOrderRequest CreateValidRequest()
        {
            return new CreateOrderRequest
            {
                ClientId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 5,
                OriginLatitude = 4.624335,
                OriginLongitude = -74.063644,
                DestinationLatitude = 6.25184,
                DestinationLongitude = -75.56359
            };
        }

        [Fact]
        public async Task Should_Fail_When_ClientDoesNotExist()
        {
            var request = CreateValidRequest();
            _clientRepositoryMock.Setup(r => r.GetByIdAsync(request.ClientId))
                .ReturnsAsync((Client)null);

            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(r => r.ClientId)
                .WithErrorMessage("El cliente no existe.");
        }

        [Fact]
        public async Task Should_Fail_When_ProductDoesNotExist()
        {
            var request = CreateValidRequest();
            _productRepositoryMock.Setup(r => r.GetByIdAsync(request.ProductId))
                .ReturnsAsync((Product)null);

            _clientRepositoryMock.Setup(r => r.GetByIdAsync(request.ClientId))
                .ReturnsAsync(new Client { Id = request.ClientId });

            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(r => r.ProductId)
                .WithErrorMessage("El producto no existe.");
        }

        [Fact]
        public async Task Should_Fail_When_OriginEqualsDestination()
        {
            var request = CreateValidRequest();
            request.DestinationLatitude = request.OriginLatitude;
            request.DestinationLongitude = request.OriginLongitude;

            _clientRepositoryMock.Setup(r => r.GetByIdAsync(request.ClientId))
                .ReturnsAsync(new Client { Id = request.ClientId });
            _productRepositoryMock.Setup(r => r.GetByIdAsync(request.ProductId))
                .ReturnsAsync(new Product { Id = request.ProductId });

            var result = await _validator.TestValidateAsync(request);

            result.ShouldHaveValidationErrorFor(x => x)
                .WithErrorMessage("El origen y destino no pueden ser iguales.");
        }

        [Fact]
        public async Task Should_Pass_With_ValidRequest()
        {
            var request = CreateValidRequest();

            _clientRepositoryMock.Setup(r => r.GetByIdAsync(request.ClientId))
                .ReturnsAsync(new Client { Id = request.ClientId });
            _productRepositoryMock.Setup(r => r.GetByIdAsync(request.ProductId))
                .ReturnsAsync(new Product { Id = request.ProductId });

            var result = await _validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
