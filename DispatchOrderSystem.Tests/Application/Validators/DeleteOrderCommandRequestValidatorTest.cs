using DispatchOrderSystem.Application.Commands.Orders;
using DispatchOrderSystem.Application.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Validators
{
    public class DeleteOrderCommandRequestValidatorTests
    {
        private readonly DeleteOrderCommandRequestValidator _validator;

        public DeleteOrderCommandRequestValidatorTests()
        {
            _validator = new DeleteOrderCommandRequestValidator();
        }

        [Fact]
        public void Should_Fail_When_OrderId_Is_Empty()
        {
            // Arrange
            var request = new DeleteOrderCommandRequest(Guid.Empty);

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.OrderId)
                  .WithErrorMessage("Order ID must not be empty.");
        }

        [Fact]
        public void Should_Pass_When_OrderId_Is_Valid()
        {
            // Arrange
            var request = new DeleteOrderCommandRequest(Guid.NewGuid());

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.OrderId);
        }
    }
}
