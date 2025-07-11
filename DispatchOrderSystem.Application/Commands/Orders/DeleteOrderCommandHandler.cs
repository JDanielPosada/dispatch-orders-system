using DispatchOrderSystem.Application.Interfaces;
using MediatR;

namespace DispatchOrderSystem.Application.Commands.Orders
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest, Unit>
    {
        private readonly IOrderService _orderService;

        public DeleteOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<Unit> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.DeleteOrderAsync(request.OrderId);
            return Unit.Value;
        }
    }
}
