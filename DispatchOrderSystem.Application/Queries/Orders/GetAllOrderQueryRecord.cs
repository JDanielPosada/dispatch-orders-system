using DispatchOrderSystem.Application.DTOs;
using MediatR;

namespace DispatchOrderSystem.Application.Queries.Orders
{
    public readonly record struct GetAllOrdersQueryRequest() : IRequest<List<OrderDetailDto>>;
}
