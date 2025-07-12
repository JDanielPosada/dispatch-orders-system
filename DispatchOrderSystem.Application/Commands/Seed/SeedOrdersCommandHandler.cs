using DispatchOrderSystem.Application.Services;
using DispatchOrderSystem.Application.Services.Interfaces;
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
