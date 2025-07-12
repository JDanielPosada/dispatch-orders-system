using DispatchOrderSystem.Application.Queries.Orders;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Validators.Orders
{
    public class GetOrderByIdQueryRequestValidator : AbstractValidator<GetOrderByIdQueryRequest>
    {
        public GetOrderByIdQueryRequestValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Order ID must not be empty.");
        }
    }
}
