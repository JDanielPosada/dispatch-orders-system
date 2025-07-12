using ClosedXML.Excel;
using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Services;
using Xunit;

namespace DispatchOrderSystem.Tests.Application.Services
{
    public class ExcelExportServiceTests
    {
        private readonly ExcelExportService _excelExportService;

        public ExcelExportServiceTests()
        {
            _excelExportService = new ExcelExportService();
        }

        [Fact]
        public void GenerateClientDistanceReportExcel_ReturnsNonEmptyByteArray()
        {
            // Arrange
            var reportData = new List<ClientDistanceReportDto>
            {
                new ClientDistanceReportDto
                {
                    ClientName = "Cliente A",
                    Orders_1_50_Km = 2,
                    Orders_51_200_Km = 1,
                    Orders_201_500_Km = 0,
                    Orders_501_1000_Km = 1
                }
            };

            // Act
            var excelBytes = _excelExportService.GenerateClientDistanceReportExcel(reportData);

            // Assert
            Assert.NotNull(excelBytes);
            Assert.True(excelBytes.Length > 0);
        }

        [Fact]
        public void GenerateClientDistanceReportExcel_CreatesCorrectHeadersAndData()
        {
            // Arrange
            var reportData = new List<ClientDistanceReportDto>
            {
                new ClientDistanceReportDto
                {
                    ClientName = "Cliente B",
                    Orders_1_50_Km = 5,
                    Orders_51_200_Km = 3,
                    Orders_201_500_Km = 2,
                    Orders_501_1000_Km = 0
                }
            };

            var excelBytes = _excelExportService.GenerateClientDistanceReportExcel(reportData);

            // Act
            using var stream = new MemoryStream(excelBytes);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet("Reporte por Cliente");

            // Assert headers
            Assert.Equal("Cliente", worksheet.Cell("A2").Value.ToString());
            Assert.Equal("1-50 km", worksheet.Cell("B2").Value.ToString());
            Assert.Equal("51-200 km", worksheet.Cell("C2").Value.ToString());
            Assert.Equal("201-500 km", worksheet.Cell("D2").Value.ToString());
            Assert.Equal("501-1000 km", worksheet.Cell("E2").Value.ToString());

            // Assert first row of data
            Assert.Equal("Cliente B", worksheet.Cell("A3").Value.ToString());
            Assert.Equal("5", worksheet.Cell("B3").Value.ToString());
            Assert.Equal("3", worksheet.Cell("C3").Value.ToString());
            Assert.Equal("2", worksheet.Cell("D3").Value.ToString());
            Assert.Equal("0", worksheet.Cell("E3").Value.ToString());
        }
    }
}
