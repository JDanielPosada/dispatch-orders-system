using DispatchOrderSystem.Application.Commands.Clients;
using DispatchOrderSystem.Application.Validators.Clients;
using FluentValidation.TestHelper;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Validators
{
    public class CreateClientCommandValidatorTests
    {
        private readonly CreateClientCommandValidator _validator;

        public CreateClientCommandValidatorTests()
        {
            _validator = new CreateClientCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new CreateClientCommandRequest(string.Empty);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Too_Short()
        {
            var model = new CreateClientCommandRequest("A");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Too_Long()
        {
            var model = new CreateClientCommandRequest(new string('A', 101));
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Valid()
        {
            var model = new CreateClientCommandRequest ("Cliente Válido");
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}
