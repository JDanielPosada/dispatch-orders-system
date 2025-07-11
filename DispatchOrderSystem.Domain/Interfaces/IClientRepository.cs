using DispatchOrderSystem.Domain.Aggregates.Entities;

namespace DispatchOrderSystem.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(Guid id);
        Task AddAsync(Client client);
    }
}
