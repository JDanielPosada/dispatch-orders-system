namespace DispatchOrderSystem.Application.DTOs
{
    /// <summary>
    /// Representa la información de un cliente.
    /// </summary>
    public class ClientDto
    {
        /// <summary>
        /// Identificador único del cliente.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del cliente.
        /// </summary>
        public string Name { get; set; } = default!;
    }
}
