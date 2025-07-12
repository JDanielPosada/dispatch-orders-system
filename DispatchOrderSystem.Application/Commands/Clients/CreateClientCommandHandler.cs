using DispatchOrderSystem.Application.Services.Interfaces;
using MediatR;

namespace DispatchOrderSystem.Application.Commands.Clients
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommandRequest, Guid>
    {
        private readonly IClientService _clientService;

        public CreateClientCommandHandler(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<Guid> Handle(CreateClientCommandRequest request, CancellationToken cancellationToken)
        {
            return await _clientService.CreateClientAsync(request.Name);
        }
    }
}
