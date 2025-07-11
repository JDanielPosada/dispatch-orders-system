using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Domain.Interfaces;
using DispatchOrderSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DispatchOrderSystem.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DispatchOrderSystemDbContext _context;

        public ClientRepository(DispatchOrderSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(Guid id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task AddAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

    }
}
