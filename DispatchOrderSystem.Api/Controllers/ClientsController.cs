using DispatchOrderSystem.Application.Commands.Clients;
using DispatchOrderSystem.Application.Queries.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatchOrderSystem.Api.Controllers
{
    /// <summary>
    /// Controlador para gestionar clientes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="request">Datos del cliente a crear.</param>
        /// <returns>ID del cliente creado.</returns>
        /// <response code="201">Cliente creado exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="500">Error inesperado</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientCommandRequest request)
        {
            var id = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetAll), new { id }, new { id });
        }

        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        /// <response code="200">Lista obtenida correctamente</response>
        /// <response code="500">Error inesperado</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _mediator.Send(new GetAllClientsQueryRequest());
            return Ok(clients);
        }
    }
}
