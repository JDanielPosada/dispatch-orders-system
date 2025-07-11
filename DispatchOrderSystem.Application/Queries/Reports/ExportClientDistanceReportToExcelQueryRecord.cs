using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.Queries.Reports
{
    public readonly record struct ExportClientDistanceReportToExcelQueryRequest : IRequest<byte[]>;
}
