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
        /// <response code="200">Orden creada exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="500">Error inesperado</response>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var result = await _mediator.Send(new CreateOrderCommandRequest(request));
            return Ok(result);
        }

        /// <summary>
        /// Elimina una orden existente por su ID.
        /// </summary>
        /// <param name="request">ID de la orden.</param>
        /// <returns>NoContent si se elimina correctamente.</returns>
        /// <response code="204">Orden eliminada exitosamente</response>
        /// <response code="400">ID inválido</response>
        /// <response code="500">Error inesperado</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteOrderCommandRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        /// <summary>
        /// Obtiene las órdenes asociadas a un cliente.
        /// </summary>
        /// <param name="request">ID del cliente.</param>
        /// <returns>Lista de órdenes del cliente.</returns>
        /// <response code="200">Órdenes encontradas</response>
        /// <response code="400">ID inválido</response>
        /// <response code="500">Error inesperado</response>
        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetByClientId([FromRoute] GetOrdersByClientIdQueryRequest request)
        {
            var orders = await _mediator.Send(request);
            return Ok(orders);
        }

        /// <summary>
        /// Obtiene una orden por su ID.
        /// </summary>
        /// <param name="request">ID de la orden.</param>
        /// <returns>La orden encontrada o 404 si no existe.</returns>
        /// <response code="200">Orden encontrada</response>
        /// <response code="400">ID inválido</response>
        /// <response code="404">Orden no encontrada</response>
        /// <response code="500">Error inesperado</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] GetOrderByIdQueryRequest request)
        {
            var order = await _mediator.Send(request);
            return order is null ? NotFound() : Ok(order);
        }

        /// <summary>
        /// Obtiene todas las órdenes registradas.
        /// </summary>
        /// <returns>Lista de todas las órdenes.</returns>
        /// <response code="200">Lista obtenida correctamente</response>
        /// <response code="500">Error inesperado</response>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _mediator.Send(new GetAllOrdersQueryRequest());
            return Ok(orders);
        }

        /// <summary>
        /// Obtiene un reporte por cliente agrupado por distancias.
        /// </summary>
        /// <returns>Lista agrupada por cliente y rango de distancia.</returns>
        /// <response code="200">Reporte generado</response>
        /// <response code="500">Error inesperado</response>
        [HttpGet("report/client-distance")]
        public async Task<IActionResult> GetClientDistanceReport()
        {
            var result = await _mediator.Send(new GetClientDistanceReportQueryRequest());
            return Ok(result);
        }

        /// <summary>
        /// Exporta el reporte por cliente y distancia a un archivo Excel.
        /// </summary>
        /// <returns>Archivo .xlsx del reporte</returns>
        /// <response code="200">Archivo generado exitosamente</response>
        /// <response code="500">Error al generar el archivo</response>
        [HttpGet("report/client-distance/export")]
        public async Task<IActionResult> ExportClientDistanceReportToExcel()
        {
            var fileBytes = await _mediator.Send(new ExportClientDistanceReportToExcelQueryRequest());
            return File(fileBytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ClientDistanceReport.xlsx");
        }
    }
}
