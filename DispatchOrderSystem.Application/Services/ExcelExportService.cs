using ClosedXML.Excel;
using DispatchOrderSystem.Application.DTOs;
using DispatchOrderSystem.Application.Services.Interfaces;
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
            var worksheet = workbook.Worksheets.Add("Reporte por Cliente");

            // Título
            var titleCell = worksheet.Cell("A1");
            titleCell.Value = "Reporte de Órdenes por Cliente y Rango de Distancia";
            var titleRange = worksheet.Range("A1:E1");
            titleRange.Merge();
            titleRange.Style.Font.SetBold().Font.SetFontSize(14);
            titleRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            titleRange.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            titleRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
            titleRange.Style.Alignment.WrapText = true;
            worksheet.Row(1).Height = 30; // Aumenta altura de fila del título

            // Encabezados
            var headers = new[] { "Cliente", "1-50 km", "51-200 km", "201-500 km", "501-1000 km" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(2, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.SetBold();
                cell.Style.Fill.BackgroundColor = XLColor.AliceBlue;
                cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                cell.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
            worksheet.Row(2).Height = 20; // Altura para fila de encabezados

            // Datos
            for (int i = 0; i < reportData.Count; i++)
            {
                var row = i + 3;
                var data = reportData[i];

                worksheet.Cell(row, 1).Value = data.ClientName;
                worksheet.Cell(row, 2).Value = data.Orders_1_50_Km;
                worksheet.Cell(row, 3).Value = data.Orders_51_200_Km;
                worksheet.Cell(row, 4).Value = data.Orders_201_500_Km;
                worksheet.Cell(row, 5).Value = data.Orders_501_1000_Km;

                for (int col = 1; col <= 5; col++)
                {
                    var cell = worksheet.Cell(row, col);
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    cell.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                }

                worksheet.Row(row).Height = 18; // Opcional: altura para filas de datos
            }

            // Ajustar automáticamente el ancho de las columnas
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
