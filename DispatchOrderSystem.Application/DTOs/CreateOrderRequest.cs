using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatchOrderSystem.Application.DTOs
{
    public class CreateOrderRequest
    {
        public Guid ClientId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public double OriginLatitude { get; set; }
        public double OriginLongitude { get; set; }

        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }
    }
}
