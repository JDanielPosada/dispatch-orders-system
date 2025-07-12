using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Queries.Reports;
using DispatchOrderSystem.Application.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Queries.Reports
{
    public class ExportClientDistanceReportToExcelQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsExcelByteArray()
        {
            // Arrange
            var mockOrderService = new Mock<IOrderService>();
            var mockExcelExportService = new Mock<IExcelExportService>();

            var reportData = new List<ClientDistanceReportDto>
        {
            new ClientDistanceReportDto
            {
                ClientName = "Cliente 1",
                Orders_1_50_Km = 3,
                Orders_51_200_Km = 2,
                Orders_201_500_Km = 1,
                Orders_501_1000_Km = 0
            }
        };

            var expectedBytes = new byte[] { 0x01, 0x02, 0x03 };

            mockOrderService
                .Setup(s => s.GetClientDistanceReportAsync())
                .ReturnsAsync(reportData);

            mockExcelExportService
                .Setup(s => s.GenerateClientDistanceReportExcel(reportData))
                .Returns(expectedBytes);

            var handler = new ExportClientDistanceReportToExcelQueryHandler(
                mockOrderService.Object,
                mockExcelExportService.Object
            );

            var request = new ExportClientDistanceReportToExcelQueryRequest();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBytes, result);
            mockOrderService.Verify(s => s.GetClientDistanceReportAsync(), Times.Once);
            mockExcelExportService.Verify(s => s.GenerateClientDistanceReportExcel(reportData), Times.Once);
        }
    }
}
