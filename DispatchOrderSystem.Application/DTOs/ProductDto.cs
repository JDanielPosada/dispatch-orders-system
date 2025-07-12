namespace DispatchOrderSystem.Application.DTOs
{
    /// <summary>
    /// Representa la información de un producto.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Descripción del producto.
        /// </summary>
        public string Description { get; set; } = default!;
    }
}
