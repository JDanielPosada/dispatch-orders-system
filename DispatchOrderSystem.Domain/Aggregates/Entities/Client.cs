namespace DispatchOrderSystem.Domain.Aggregates.Entities
{
    public class Client
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
