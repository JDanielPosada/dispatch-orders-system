using System.ComponentModel.DataAnnotations;

namespace DispatchOrderSystem.Web.Models.Orders
{
    /// <summary>
    /// Modelo para crear una orden desde el frontend.
    /// </summary>
    public class CreateOrderRequest
    {
        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
        public int Quantity { get; set; }

        [Range(-90, 90)]
        public double OriginLatitude { get; set; }

        [Range(-180, 180)]
        public double OriginLongitude { get; set; }

        [Range(-90, 90)]
        public double DestinationLatitude { get; set; }

        [Range(-180, 180)]
        public double DestinationLongitude { get; set; }
    }
}
