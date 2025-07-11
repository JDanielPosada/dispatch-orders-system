using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Interfaces;
using MediatR;

namespace DispatchOrderSystem.Application.Queries.Orders
{
    internal class GetAllOrderQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, List<OrderDetailDto>>
    {
        private readonly IOrderService _orderService;

        public GetAllOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<OrderDetailDto>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            return await _orderService.GetAllOrderDetailsAsync();
        }
    }
}
