using DispatchOrderSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Interfaces
{
    public interface IExcelExportService
    {
        byte[] GenerateClientDistanceReportExcel(List<ClientDistanceReportDto> reportData);
    }
}
