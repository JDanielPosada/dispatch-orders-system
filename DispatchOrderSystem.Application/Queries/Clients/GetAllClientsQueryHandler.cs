using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Services.Interfaces;
using MediatR;

namespace DispatchOrderSystem.Application.Queries.Clients
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQueryRequest, IEnumerable<ClientDto>>
    {
        private readonly IClientService _clientService;

        public GetAllClientsQueryHandler(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQueryRequest request, CancellationToken cancellationToken)
        {
            return await _clientService.GetAllClientsAsync();
        }
    }
}
