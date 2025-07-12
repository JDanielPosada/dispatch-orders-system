using MediatR;

namespace DispatchOrderSystem.Application.Commands.Products
{
    public record CreateProductCommandRequest(string Name, string Description) : IRequest<Guid>;

}
