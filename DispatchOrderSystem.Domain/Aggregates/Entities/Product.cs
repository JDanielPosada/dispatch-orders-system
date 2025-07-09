namespace DispatchOrderSystem.Domain.Aggregates.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!; 
    }
}
