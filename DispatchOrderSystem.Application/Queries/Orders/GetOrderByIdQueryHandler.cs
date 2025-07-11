using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Queries.Orders
{
    internal class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQueryRequest, OrderDetailDto?>
    {
        private readonly IOrderService _orderService;

        public GetOrderByIdHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<OrderDetailDto?> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            return await _orderService.GetOrderDetailByIdAsync(request.OrderId);
        }
    }
}
