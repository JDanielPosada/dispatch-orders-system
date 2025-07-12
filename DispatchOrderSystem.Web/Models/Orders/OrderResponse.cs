namespace DispatchOrderSystem.Web.Models.Orders
{
    /// <summary>
    /// Modelo para mostrar los detalles de una orden.
    /// </summary>
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        public double DistanceKm { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
