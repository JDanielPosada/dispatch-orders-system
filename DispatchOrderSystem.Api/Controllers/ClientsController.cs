using DispatchOrderSystem.Application.Commands.Clients;
using DispatchOrderSystem.Application.Queries.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatchOrderSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientCommandRequest request)
        {
            var id = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetAll), new { id }, new { id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _mediator.Send(new GetAllClientsQueryRequest());
            return Ok(clients);
        }
    }
}
