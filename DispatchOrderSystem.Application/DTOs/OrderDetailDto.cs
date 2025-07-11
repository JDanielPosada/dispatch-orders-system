namespace DispatchOrderSystem.Application.DTOs
{
    /// <summary>
    /// Detalle completo de una orden de despacho.
    /// </summary>
    public class OrderDetailDto
    {
        /// <summary>
        /// Identificador único de la orden.
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Nombre del cliente asociado a la orden.
        /// </summary>
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del producto solicitado.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Distancia calculada en kilómetros entre origen y destino.
        /// </summary>
        public double DistanceKm { get; set; }

        /// <summary>
        /// Costo calculado del despacho.
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Fecha de creación de la orden.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
