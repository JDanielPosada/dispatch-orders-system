using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.DTOs
{
    public class OrderDetailDto
    {
        public Guid OrderId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public double DistanceKm { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
