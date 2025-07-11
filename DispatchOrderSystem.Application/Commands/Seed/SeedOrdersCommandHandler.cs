using DispatchOrderSystem.Application.Interfaces;
using DispatchOrderSystem.Application.Services;
using MediatR;

namespace DispatchOrderSystem.Application.Commands.Seed
{
    public class SeedOrdersHandler : IRequestHandler<SeedOrdersCommandRequest, string>
    {
        private readonly ISeedService _seedService;

        public SeedOrdersHandler(ISeedService seedService)
        {
            _seedService = seedService;
        }

        public async Task<string> Handle(SeedOrdersCommandRequest request, CancellationToken cancellationToken)
        {
            return await _seedService.SeedOrdersAsync();
        }
    }
}
