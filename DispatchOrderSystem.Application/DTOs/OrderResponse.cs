namespace DispatchOrderSystem.Application.DTOs
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public string ClientName { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public double DistanceKm { get; set; }
        public decimal Cost { get; set; }
    }
}
