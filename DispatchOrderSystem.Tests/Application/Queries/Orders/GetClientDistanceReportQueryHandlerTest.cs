using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Queries.Orders;
using DispatchOrderSystem.Application.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Queries.Orders
{
    public class GetClientDistanceReportQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsClientDistanceReportList()
        {
            // Arrange
            var expectedReport = new List<ClientDistanceReportDto>
        {
            new ClientDistanceReportDto
            {
                ClientName = "Cliente A",
                Orders_1_50_Km = 2,
                Orders_51_200_Km = 3,
                Orders_201_500_Km = 0,
                Orders_501_1000_Km = 1
            },
            new ClientDistanceReportDto
            {
                ClientName = "Cliente B",
                Orders_1_50_Km = 0,
                Orders_51_200_Km = 0,
                Orders_201_500_Km = 5,
                Orders_501_1000_Km = 2
            }
        };

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(service => service.GetClientDistanceReportAsync())
                .ReturnsAsync(expectedReport);

            var handler = new GetClientDistanceReportQueryHandler(mockOrderService.Object);
            var request = new GetClientDistanceReportQueryRequest();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(expectedReport.Count, result.Count);
            Assert.Equal("Cliente A", result[0].ClientName);
            Assert.Equal(6, result[0].TotalOrders); // 2 + 3 + 0 + 1
            Assert.Equal(7, result[1].TotalOrders); // 0 + 0 + 5 + 2
            mockOrderService.Verify(s => s.GetClientDistanceReportAsync(), Times.Once);
        }
    }
}
