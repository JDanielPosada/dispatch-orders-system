using DispatchOrderSystem.Application.Interfaces;
using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DispatchOrderSystem.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DispatchOrderSystemDbContext _context;

        public OrderRepository(DispatchOrderSystemDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Product)
                .Where(o => o.ClientId == clientId)
                .ToListAsync();
        }

        public async Task DeleteAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Product)
                .ToListAsync();
        }
    }
}
