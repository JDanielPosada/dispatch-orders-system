using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Commands.Orders
{
    public readonly record struct DeleteOrderCommandRequest(Guid OrderId) : IRequest<Unit>;
}
