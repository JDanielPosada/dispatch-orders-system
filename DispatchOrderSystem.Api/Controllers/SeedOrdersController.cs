using DispatchOrderSystem.Application.Commands.Seed;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatchOrderSystem.Api.Controllers
{
    /// <summary>
    /// Controlador para insertar datos de prueba (semillas) en la base de datos.
    /// Solo debe usarse en entornos de desarrollo.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SeedOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeedOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Inserta clientes, productos y órdenes de prueba en la base de datos.
        /// </summary>
        /// <remarks>
        /// Este endpoint solo debe utilizarse en un entorno de desarrollo o pruebas.
        /// Inserta datos predefinidos para facilitar el desarrollo de la aplicación.
        /// </remarks>
        /// <returns>Resultado de la operación de seed.</returns>
        /// <response code="200">Datos sembrados correctamente</response>
        /// <response code="500">Error al insertar los datos</response>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var result = await _mediator.Send(new SeedOrdersCommandRequest());
            return Ok(new { message = result });
        }
    }
}
