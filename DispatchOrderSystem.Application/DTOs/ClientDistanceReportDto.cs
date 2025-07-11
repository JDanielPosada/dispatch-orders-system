using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.DTOs
{
    public class ClientDistanceReportDto
    {
        public string ClientName { get; set; } = string.Empty;

        public int Orders_1_50_Km { get; set; }
        public int Orders_51_200_Km { get; set; }
        public int Orders_201_500_Km { get; set; }
        public int Orders_501_1000_Km { get; set; }
        public int TotalOrders => Orders_1_50_Km + Orders_51_200_Km + Orders_201_500_Km + Orders_501_1000_Km;
    }
}
