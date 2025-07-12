using DispatchOrderSystem.Application.Queries.Orders;
using DispatchOrderSystem.Application.Validators.Orders;
using FluentValidation.TestHelper;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Validators
{
    public class GetOrdersByClientIdQueryRequestValidatorTests
    {
        private readonly GetOrdersByClientIdQueryRequestValidator _validator;

        public GetOrdersByClientIdQueryRequestValidatorTests()
        {
            _validator = new GetOrdersByClientIdQueryRequestValidator();
        }

        [Fact]
        public void Should_Fail_When_ClientId_Is_Empty()
        {
            // Arrange
            var request = new GetOrdersByClientIdQueryRequest(Guid.Empty);

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ClientId)
                  .WithErrorMessage("Client ID must not be empty.");
        }

        [Fact]
        public void Should_Pass_When_ClientId_Is_Valid()
        {
            // Arrange
            var request = new GetOrdersByClientIdQueryRequest(Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ClientId);
        }
    }
}
