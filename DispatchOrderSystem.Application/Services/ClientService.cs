using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Services.Interfaces;
using DispatchOrderSystem.Domain.Aggregates.Entities;
using DispatchOrderSystem.Domain.Interfaces;

namespace DispatchOrderSystem.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return clients.Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<bool> ExistsAsync(Guid clientId)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);
            return client != null;
        }

        public async Task<Guid> CreateClientAsync(string name)
        {
            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            await _clientRepository.AddAsync(client);
            return client.Id;
        }
    }
}
