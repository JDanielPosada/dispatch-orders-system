using DispatchOrderSystem.Web.Models.Reports;
using DispatchOrderSystem.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DispatchOrderSystem.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> ClientDistance()
        {
            var data = await _reportService.GetClientDistanceReportAsync();
            var viewModel = new ClientDistanceReportListViewModel
            {
                ReportData = data
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ExportClientDistanceToExcel()
        {
            var bytes = await _reportService.ExportClientDistanceReportToExcelAsync();
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ClientDistanceReport.xlsx");
        }
    }
}
