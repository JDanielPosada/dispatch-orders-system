using DispatchOrderSystem.Application.DTOs;
using MediatR;

namespace DispatchOrderSystem.Application.Commands.Orders
{
    public readonly record struct CreateOrderCommandRequest(CreateOrderRequest Request) : IRequest<OrderResponse> { }

}
