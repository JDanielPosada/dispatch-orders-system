using DispatchOrderSystem.Application.Queries.Orders;
using FluentValidation;

namespace DispatchOrderSystem.Application.Validators.Orders
{
    public class GetOrdersByClientIdQueryRequestValidator : AbstractValidator<GetOrdersByClientIdQueryRequest>
    {
        public GetOrdersByClientIdQueryRequestValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("Client ID must not be empty.");
        }
    }
}
