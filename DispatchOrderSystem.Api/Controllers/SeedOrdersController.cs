using DispatchOrderSystem.Application.Commands.Seed;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatchOrderSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeedOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var result = await _mediator.Send(new SeedOrdersCommandRequest());
            return Ok(new { message = result });
        }
    }
}
