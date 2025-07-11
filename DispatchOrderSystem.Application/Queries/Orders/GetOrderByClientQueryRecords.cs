using DispatchOrderSystem.Application.DTOs;
using MediatR;

namespace DispatchOrderSystem.Application.Queries.Orders
{
    public record GetOrdersByClientIdQueryRequest(Guid ClientId) : IRequest<List<OrderDetailDto>>;

}
