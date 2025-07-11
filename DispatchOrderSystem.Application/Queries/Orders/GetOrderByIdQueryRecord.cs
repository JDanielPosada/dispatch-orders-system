using DispatchOrderSystem.Application.DTOs;
using MediatR;

namespace DispatchOrderSystem.Application.Queries.Orders
{
    public record GetOrderByIdQueryRequest(Guid OrderId) : IRequest<OrderDetailDto?>;

}
