using DispatchOrderSystem.Api.Controllers;
using DispatchOrderSystem.Application.Commands.Orders;
using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Queries.Orders;
using DispatchOrderSystem.Application.Queries.Reports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DispatchOrderSystem.Tests.Api.Controllers
{
    public class OrdersControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new OrdersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateOrder_ReturnsOk_WithOrderResponse()
        {
            // Arrange
            var request = new CreateOrderRequest
            {
                ClientId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 5,
                OriginLatitude = 2.0,
                OriginLongitude = -76.5,
                DestinationLatitude = 3.0,
                DestinationLongitude = -76.6
            };

            var expectedResponse = new OrderResponse
            {
                OrderId = Guid.NewGuid(),
                ClientName = "Cliente Prueba",
                ProductName = "Producto Prueba",
                DistanceKm = 150.0,
                Cost = 50000
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<CreateOrderCommandRequest>(c => c.Request == request), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CreateOrder(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<OrderResponse>(okResult.Value);
            Assert.Equal(expectedResponse.OrderId, response.OrderId);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenOrderExists()
        {
            var orderId = Guid.NewGuid();
            var expectedOrder = new OrderDetailDto
            {
                OrderId = orderId,
                ClientName = "Cliente Demo",
                ProductName = "Producto Demo",
                DistanceKm = 100,
                Cost = 45000,
                CreatedAt = DateTime.UtcNow
            };

            _mediatorMock
                .Setup(m => m.Send(new GetOrderByIdQueryRequest(orderId), default))
                .ReturnsAsync(expectedOrder);

            var result = await _controller.GetById(new GetOrderByIdQueryRequest(orderId));

            var okResult = Assert.IsType<OkObjectResult>(result);
            var order = Assert.IsType<OrderDetailDto>(okResult.Value);
            Assert.Equal(orderId, order.OrderId);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            var orderId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(new GetOrderByIdQueryRequest(orderId), default))
                .ReturnsAsync((OrderDetailDto?)null);

            var result = await _controller.GetById(new GetOrderByIdQueryRequest(orderId));

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetByClientId_ReturnsOk_WithOrderList()
        {
            var clientId = Guid.NewGuid();
            var orders = new List<OrderDetailDto>
            {
                new() { OrderId = Guid.NewGuid(), ClientName = "Cliente A", ProductName = "Prod A", DistanceKm = 10, Cost = 1000, CreatedAt = DateTime.UtcNow },
                new() { OrderId = Guid.NewGuid(), ClientName = "Cliente A", ProductName = "Prod B", DistanceKm = 20, Cost = 2000, CreatedAt = DateTime.UtcNow }
            };

            _mediatorMock
                .Setup(m => m.Send(new GetOrdersByClientIdQueryRequest(clientId), default))
                .ReturnsAsync(orders);

            var result = await _controller.GetByClientId(new GetOrdersByClientIdQueryRequest(clientId));

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedOrders = Assert.IsAssignableFrom<List<OrderDetailDto>>(okResult.Value);
            Assert.Equal(2, returnedOrders.Count);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithOrderList()
        {
            var orders = new List<OrderDetailDto>
            {
                new() { OrderId = Guid.NewGuid(), ClientName = "Cliente A", ProductName = "Prod A", DistanceKm = 10, Cost = 1000, CreatedAt = DateTime.UtcNow },
                new() { OrderId = Guid.NewGuid(), ClientName = "Cliente B", ProductName = "Prod B", DistanceKm = 20, Cost = 2000, CreatedAt = DateTime.UtcNow }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllOrdersQueryRequest>(), default))
                .ReturnsAsync(orders);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedOrders = Assert.IsAssignableFrom<List<OrderDetailDto>>(okResult.Value);
            Assert.Equal(2, returnedOrders.Count);
        }

        [Fact]
        public async Task DeleteOrder_ReturnsNoContent()
        {
            var orderId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(new DeleteOrderCommandRequest(orderId), default))
                .Returns(Task.FromResult(Unit.Value));

            var result = await _controller.Delete(new DeleteOrderCommandRequest(orderId));

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetClientDistanceReport_ReturnsOk_WithReportList()
        {
            var report = new List<ClientDistanceReportDto>
            {
                new() { ClientName = "Cliente 1", Orders_1_50_Km = 2, Orders_51_200_Km = 3, Orders_201_500_Km = 1, Orders_501_1000_Km = 0 },
                new() { ClientName = "Cliente 2", Orders_1_50_Km = 0, Orders_51_200_Km = 0, Orders_201_500_Km = 2, Orders_501_1000_Km = 1 }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetClientDistanceReportQueryRequest>(), default))
                .ReturnsAsync(report);

            var result = await _controller.GetClientDistanceReport();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedReport = Assert.IsAssignableFrom<List<ClientDistanceReportDto>>(okResult.Value);
            Assert.Equal(2, returnedReport.Count);
        }

        [Fact]
        public async Task ExportClientDistanceReportToExcel_ReturnsFile()
        {
            var fileBytes = new byte[] { 0x01, 0x02, 0x03 };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<ExportClientDistanceReportToExcelQueryRequest>(), default))
                .ReturnsAsync(fileBytes);

            var result = await _controller.ExportClientDistanceReportToExcel();

            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileResult.ContentType);
            Assert.Equal("ClientDistanceReport.xlsx", fileResult.FileDownloadName);
            Assert.Equal(fileBytes, fileResult.FileContents);
        }
    }
}
