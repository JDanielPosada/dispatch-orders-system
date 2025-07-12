using DispatchOrderSystem.Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Queries.Reports
{
    public class ExportClientDistanceReportToExcelQueryHandler : IRequestHandler<ExportClientDistanceReportToExcelQueryRequest, byte[]>
    {
        private readonly IOrderService _orderService;
        private readonly IExcelExportService _excelExportService;

        public ExportClientDistanceReportToExcelQueryHandler(
            IOrderService orderService,
            IExcelExportService excelExportService)
        {
            _orderService = orderService;
            _excelExportService = excelExportService;
        }

        public async Task<byte[]> Handle(ExportClientDistanceReportToExcelQueryRequest request, CancellationToken cancellationToken)
        {
            var report = await _orderService.GetClientDistanceReportAsync();
            var excelBytes = _excelExportService.GenerateClientDistanceReportExcel(report);
            return excelBytes;
        }
    }
}
