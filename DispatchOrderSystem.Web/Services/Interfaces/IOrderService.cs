using DispatchOrderSystem.Web.Models.Orders;

namespace DispatchOrderSystem.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetOrdersByClientIdAsync(Guid clientId);
        Task CreateOrderAsync(CreateOrderRequest request);

        Task<List<OrderResponse>> GetAllOrdersAsync();
    }
}
