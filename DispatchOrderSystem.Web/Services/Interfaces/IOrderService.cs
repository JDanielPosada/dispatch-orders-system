using DispatchOrderSystem.Web.Models;

namespace DispatchOrderSystem.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetOrdersByClientIdAsync(Guid clientId);
        Task CreateOrderAsync(CreateOrderRequest request);
    }
}
