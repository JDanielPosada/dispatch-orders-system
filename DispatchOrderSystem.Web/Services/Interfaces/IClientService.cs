using DispatchOrderSystem.Web.Models.Clients;

namespace DispatchOrderSystem.Web.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task CreateAsync(CreateClientViewModel product);

    }
}
