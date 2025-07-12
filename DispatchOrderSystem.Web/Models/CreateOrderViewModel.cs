using DispatchOrderSystem.Web.Models.Clients;
using DispatchOrderSystem.Web.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace DispatchOrderSystem.Web.Models
{
    public class CreateOrderViewModel
    {
        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
        public int Quantity { get; set; }

        [Required] public double OriginLatitude { get; set; }
        [Required] public double OriginLongitude { get; set; }
        [Required] public double DestinationLatitude { get; set; }
        [Required] public double DestinationLongitude { get; set; }

        public IEnumerable<ClientDto>? Clients { get; set; }
        public IEnumerable<ProductDto>? Products { get; set; }
    }
}
