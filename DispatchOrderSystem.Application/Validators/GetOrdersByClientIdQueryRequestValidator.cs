using DispatchOrderSystem.Application.Queries.Orders;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Validators
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
