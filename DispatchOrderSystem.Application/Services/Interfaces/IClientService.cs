using DispatchOrderSystem.Application.DTOs;

namespace DispatchOrderSystem.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetAllClientsAsync();
        Task<bool> ExistsAsync(Guid clientId);
        Task<Guid> CreateClientAsync(string name);

    }
}
