using DispatchOrderSystem.Web.Models.Reports;
using DispatchOrderSystem.Web.Services.Interfaces;

namespace DispatchOrderSystem.Web.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _http;

        public ReportService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("DispatchOrderApi");
        }

        public async Task<List<ClientDistanceReportDto>> GetClientDistanceReportAsync()
        {
            var result = await _http.GetFromJsonAsync<List<ClientDistanceReportDto>>("/api/orders/report/client-distance");
            return result ?? new List<ClientDistanceReportDto>();
        }

        public async Task<byte[]> ExportClientDistanceReportToExcelAsync()
        {
            var response = await _http.GetAsync("/api/orders/report/client-distance/export");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
