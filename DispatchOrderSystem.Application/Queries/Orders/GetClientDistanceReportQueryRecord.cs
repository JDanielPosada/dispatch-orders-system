using DispatchOrderSystem.Application.DTOs;
using MediatR;

namespace DispatchOrderSystem.Application.Queries.Orders
{
    public readonly record struct GetClientDistanceReportQueryRequest() : IRequest<List<ClientDistanceReportDto>>;

}
