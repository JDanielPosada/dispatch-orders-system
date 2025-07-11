using DispatchOrderSystem.Application.Commands.Orders;
using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Queries.Orders;
using DispatchOrderSystem.Application.Queries.Reports;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace DispatchOrderSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea una nueva orden de despacho.
        /// </summary>
        /// <param name="request">Datos de la orden.</param>
        /// <returns>Resumen de la orden creada.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var result = await _mediator.Send(new CreateOrderCommandRequest(request));
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetByClientId(Guid clientId)
        {
            try
            {
                var orders = await _mediator.Send(new GetOrdersByClientIdQueryRequest(clientId));
                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var order = await _mediator.Send(new GetOrderByIdQueryRequest(id));
                return order is null ? NotFound() : Ok(order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteOrderCommandRequest(id));
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orders = await _mediator.Send(new GetAllOrdersQueryRequest());
                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        [HttpGet("report/client-distance")]
        public async Task<IActionResult> GetClientDistanceReport()
        {
            try
            {
                var result = await _mediator.Send(new GetClientDistanceReportQueryRequest());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", detail = ex.Message });
            }
        }

        [HttpGet("report/client-distance/export")]
        public async Task<IActionResult> ExportClientDistanceReportToExcel()
        {
            try
            {
                var fileBytes = await _mediator.Send(new ExportClientDistanceReportToExcelQueryRequest());

                return File(fileBytes,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "ClientDistanceReport.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while generating the report.", detail = ex.Message });
            }
        }
    }
}
