using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Services.Interfaces;
using MediatR;

namespace DispatchOrderSystem.Application.Queries.Orders
{
    public class GetClientDistanceReportQueryHandler : IRequestHandler<GetClientDistanceReportQueryRequest, List<ClientDistanceReportDto>>
    {
        private readonly IOrderService _orderService;

        public GetClientDistanceReportQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<ClientDistanceReportDto>> Handle(GetClientDistanceReportQueryRequest request, CancellationToken cancellationToken)
        {
            return await _orderService.GetClientDistanceReportAsync();
        }
    }
}
