using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Interfaces;
using MediatR;

namespace DispatchOrderSystem.Application.Commands.Orders
{
    public class OrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, OrderResponse>
    {
        private readonly IOrderService _orderService;
        public OrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<OrderResponse> Handle(CreateOrderCommandRequest command, CancellationToken cancellationToken)
        {
            return await _orderService.CreateOrderAsync(command.Request);
        }
    }
}
