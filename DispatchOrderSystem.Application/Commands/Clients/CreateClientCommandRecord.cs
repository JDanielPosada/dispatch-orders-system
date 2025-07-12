using MediatR;

namespace DispatchOrderSystem.Application.Commands.Clients
{
    public record CreateClientCommandRequest(string Name) : IRequest<Guid>;

}
