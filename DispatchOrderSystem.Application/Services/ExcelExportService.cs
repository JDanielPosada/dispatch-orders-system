using ClosedXML.Excel;
using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Services
{
    public class ExcelExportService : IExcelExportService
    {
        public byte[] GenerateClientDistanceReportExcel(List<ClientDistanceReportDto> reportData)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Client Distance Report");

            // Add headers
            worksheet.Cell(1, 1).Value = "Client";
            worksheet.Cell(1, 2).Value = "1-50 km";
            worksheet.Cell(1, 3).Value = "51-200 km";
            worksheet.Cell(1, 4).Value = "201-500 km";
            worksheet.Cell(1, 5).Value = "501-1000 km";

            // Add data
            for (int i = 0; i < reportData.Count; i++)
            {
                var row = i + 2; // Start from the second row;
                var data = reportData[i];
                worksheet.Cell(row, 1).Value = data.ClientName;
                worksheet.Cell(row, 2).Value = data.Orders_1_50_Km;
                worksheet.Cell(row, 3).Value = data.Orders_51_200_Km;
                worksheet.Cell(row, 4).Value = data.Orders_201_500_Km;
                worksheet.Cell(row, 5).Value = data.Orders_501_1000_Km;
            }

            // Adjust column widths
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
