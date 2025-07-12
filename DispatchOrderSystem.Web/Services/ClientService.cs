using DispatchOrderSystem.Web.Models.Clients;
using DispatchOrderSystem.Web.Services.Interfaces;

namespace DispatchOrderSystem.Web.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _http;

        public ClientService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("DispatchOrderApi");
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var result = await _http.GetFromJsonAsync<IEnumerable<ClientDto>>("/api/clients");
            return result ?? Enumerable.Empty<ClientDto>();
        }
        public async Task CreateAsync(CreateClientViewModel client)
        {
            var response = await _http.PostAsJsonAsync("/api/clients", client);
            response.EnsureSuccessStatusCode();
        }
    }
}
