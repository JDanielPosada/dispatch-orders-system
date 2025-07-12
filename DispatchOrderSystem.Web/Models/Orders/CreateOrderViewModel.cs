using DispatchOrderSystem.Web.Models.Clients;
using DispatchOrderSystem.Web.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace DispatchOrderSystem.Web.Models.Orders
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public Guid ClientId { get; set; }

        [Required(ErrorMessage = "El producto es obligatorio.")]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "La latitud de origen es obligatoria.")]
        [Range(-90, 90, ErrorMessage = "La latitud de origen debe estar entre -90 y 90.")]
        public double OriginLatitude { get; set; }

        [Required(ErrorMessage = "La longitud de origen es obligatoria.")]
        [Range(-180, 180, ErrorMessage = "La longitud de origen debe estar entre -180 y 180.")]
        public double OriginLongitude { get; set; }

        [Required(ErrorMessage = "La latitud de destino es obligatoria.")]
        [Range(-90, 90, ErrorMessage = "La latitud de Destino debe estar entre -90 y 90.")]
        public double DestinationLatitude { get; set; }

        [Required(ErrorMessage = "La longitud de destino es obligatoria.")]
        [Range(-180, 180, ErrorMessage = "La longitud de destino debe estar entre -180 y 180.")]
        public double DestinationLongitude { get; set; }

        public IEnumerable<ClientDto>? Clients { get; set; }
        public IEnumerable<ProductDto>? Products { get; set; }
    }
}
