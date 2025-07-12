using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Services.Interfaces;
using MediatR;

namespace DispatchOrderSystem.Application.Queries.Orders
{
    public class GetOrdersByClientIdHandler
        : IRequestHandler<GetOrdersByClientIdQueryRequest, List<OrderDetailDto>>
    {
        private readonly IOrderService _orderService;

        public GetOrdersByClientIdHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<OrderDetailDto>> Handle(GetOrdersByClientIdQueryRequest request, CancellationToken cancellationToken)
        {
            return await _orderService.GetOrdersByClientIdAsync(request.ClientId);
        }
    }
}
