using DispatchOrderSystem.Application.Commands.Products;
using DispatchOrderSystem.Application.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatchOrderSystem.Api.Controllers
{
    /// <summary>
    /// Controlador para gestionar productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="request">Datos del producto a crear.</param>
        /// <returns>ID del producto creado.</returns>
        /// <response code="201">Producto creado exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="500">Error inesperado</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommandRequest request)
        {
            var id = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetAll), new { id }, new { id });
        }

        /// <summary>
        /// Obtiene todos los productos registrados.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        /// <response code="200">Lista obtenida correctamente</response>
        /// <response code="500">Error inesperado</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQueryRequest());
            return Ok(products);
        }
    }
}
