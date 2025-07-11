using DispatchOrderSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Queries.Orders
{
    public readonly record struct GetOrderByIdQueryRequest(Guid OrderId) : IRequest<OrderDetailDto?>;

}
