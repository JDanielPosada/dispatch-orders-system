using DispatchOrderSystem.Application.DTOs;
using MediatR;

namespace DispatchOrderSystem.Application.Commands.Seed
{
    public readonly record struct SeedOrdersCommandRequest() : IRequest<string> { }

}
