using DispatchOrderSystem.Application.Commands.Products;
using DispatchOrderSystem.Application.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DispatchOrderSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommandRequest request)
        {
            var id = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetAll), new { id }, new { id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQueryRequest());
            return Ok(products);
        }
    }
}
