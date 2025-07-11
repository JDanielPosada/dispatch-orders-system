using MediatR;

namespace DispatchOrderSystem.Application.Commands.Orders
{
    public record DeleteOrderCommandRequest(Guid OrderId) : IRequest<Unit>;
}
