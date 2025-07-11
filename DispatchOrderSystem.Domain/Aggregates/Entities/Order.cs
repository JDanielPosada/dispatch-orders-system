using DispatchOrderSystem.Domain.Aggregates.ValueObjects;

namespace DispatchOrderSystem.Domain.Aggregates.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ClientId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public Coordinates Origin { get; set; } = default!;
        public Coordinates Destination { get; set; } = default!;

        public double DistanceKm { get; set; }
        public decimal Cost { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relations
        public Client Client { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}
