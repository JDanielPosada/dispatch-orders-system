using DispatchOrderSystem.Web.Models.Reports;

namespace DispatchOrderSystem.Web.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<ClientDistanceReportDto>> GetClientDistanceReportAsync();
        Task<byte[]> ExportClientDistanceReportToExcelAsync();
    }
}
