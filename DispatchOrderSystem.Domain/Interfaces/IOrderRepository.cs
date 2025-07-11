using DispatchOrderSystem.Domain.Aggregates.Entities;

namespace DispatchOrderSystem.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId);
        Task<Order?> GetByIdAsync(Guid orderId);
        Task DeleteAsync(Guid orderId);
        Task<List<Order>> GetAllAsync();

    }
}
